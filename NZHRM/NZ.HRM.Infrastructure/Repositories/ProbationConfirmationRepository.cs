using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Common;
using NZ.HRM.Application.Employees.Commands.ConfirmProbationEmployees;
using NZ.HRM.Application.Employees.Queries.GetProbationCompletion;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Services;
using NZ.HRM.Infrastructure.Persistence;
using System.Text;
using EmployeeStatusEnum = NZ.HRM.Utility.Enum.EmployeeStatus;
using DocumentTypeEnum = NZ.HRM.Utility.Enum.DocumentType;

namespace NZ.HRM.Infrastructure.Repositories;

public class ProbationConfirmationRepository : IProbationConfirmationRepository
{
    private readonly ApplicationDbContext _context;

    public ProbationConfirmationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProbationCompletionPagedResultDto> GetEligibleEmployeesAsync(
        GetProbationCompletionQuery query,
        CancellationToken cancellationToken = default)
    {
        var businessDate = query.ProbationEndDate ?? DateOnly.FromDateTime(DateTime.UtcNow);

        var departmentExists = true;
        if (!string.IsNullOrWhiteSpace(query.DepartmentId))
        {
            departmentExists = await _context.MstDepartments
                .AsNoTracking()
                .AnyAsync(d => d.Id == query.DepartmentId && d.IsActive, cancellationToken);
        }

        if (!departmentExists)
            throw new BusinessRuleException("INVALID_DEPARTMENT", "Invalid department selected.");

        var sectionExists = true;
        if (!string.IsNullOrWhiteSpace(query.SectionId))
        {
            sectionExists = await _context.MstSections
                .AsNoTracking()
                .AnyAsync(s => s.Id == query.SectionId && s.IsActive, cancellationToken);
        }

        if (!sectionExists)
            throw new BusinessRuleException("INVALID_SECTION", "Invalid section selected.");

        var baseQuery =
            from employee in _context.HrmEmployeeMasters.AsNoTracking()
            join employment in _context.HrmEmployeeEmployments.AsNoTracking()
                on employee.Id equals employment.EmployeeId
            join department in _context.MstDepartments.AsNoTracking()
                on employment.DepartmentId equals department.Id into departmentJoin
            from department in departmentJoin.DefaultIfEmpty()
            join section in _context.MstSections.AsNoTracking()
                on employment.SectionId equals section.Id into sectionJoin
            from section in sectionJoin.DefaultIfEmpty()
            where employee.IsActive
                  && employment.JoiningDate != null
                  && employment.ProbationPeriod != null
                  && employment.ConfirmationDate == null
                  && (string.IsNullOrWhiteSpace(query.DepartmentId) || employment.DepartmentId == query.DepartmentId)
                  && (string.IsNullOrWhiteSpace(query.SectionId) || employment.SectionId == query.SectionId)
            select new
            {
                employee.Id,
                employee.EmployeeCode,
                employee.EmployeeName,
                Department = department != null ? department.DepartmentName : string.Empty,
                Section = section != null ? section.SectionName : string.Empty,
                JoiningDate = employment.JoiningDate!.Value,
                ProbationPeriod = employment.ProbationPeriod!.Value
            };

        var candidates = await baseQuery
            .OrderBy(x => x.JoiningDate)
            .ThenBy(x => x.EmployeeCode)
            .ToListAsync(cancellationToken);

        var eligible = candidates
            .Select(x => new ProbationCompletionListItemDto
            {
                EmployeeId = x.Id,
                EmployeeCode = x.EmployeeCode,
                EmployeeName = x.EmployeeName,
                Department = x.Department,
                Section = x.Section,
                DateOfJoining = x.JoiningDate,
                ProbationEndDate = x.JoiningDate.AddMonths((int)x.ProbationPeriod),
                Status = "COMPLETED",
                ConfirmationEligible = true
            })
            .Where(x => x.ProbationEndDate <= businessDate)
            .OrderBy(x => x.ProbationEndDate)
            .ThenBy(x => x.EmployeeCode)
            .ToList();

        var total = eligible.Count;
        var items = eligible
            .Skip(Math.Max(0, (query.PageNumber - 1)) * Math.Max(1, query.PageSize))
            .Take(Math.Max(1, query.PageSize))
            .ToList();

        return new ProbationCompletionPagedResultDto
        {
            Total = total,
            Items = items
        };
    }

    public async Task<ProbationConfirmationBatchResultDto> ConfirmEmployeesAsync(
        ConfirmProbationEmployeesCommand command,
        string confirmedBy,
        CancellationToken cancellationToken = default)
    {
        var employeeIds = command.EmployeeIds
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (employeeIds.Count == 0)
            throw new BusinessRuleException("INVALID_REQUEST", "At least one employee must be selected for confirmation.");

        var employees = await _context.HrmEmployeeMasters
            .Include(e => e.Employment)
            .Include(e => e.Personal)
            .Where(e => employeeIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        var employeeById = employees.ToDictionary(e => e.Id, StringComparer.OrdinalIgnoreCase);
        var missingIds = employeeIds.Where(id => !employeeById.ContainsKey(id)).ToList();
        if (missingIds.Count > 0)
            throw new BusinessRuleException("EMPLOYEE_NOT_FOUND", $"Employee record does not exist: {string.Join(", ", missingIds)}");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        foreach (var employee in employees)
        {
            if (employee.Employment is null || employee.Employment.JoiningDate is null || employee.Employment.ProbationPeriod is null)
                throw new BusinessRuleException("EMPLOYEE_NOT_ELIGIBLE", "Employee has not completed probation period.");

            var probationEndDate = employee.Employment.JoiningDate.Value.AddMonths((int)employee.Employment.ProbationPeriod.Value);
            if (probationEndDate > today)
                throw new BusinessRuleException("EMPLOYEE_NOT_ELIGIBLE", "Employee has not completed probation period.");

            if (employee.Employment.ConfirmationDate.HasValue)
                throw new BusinessRuleException("EMPLOYEE_ALREADY_CONFIRMED", "Employee already confirmed.");
        }

        var result = new ProbationConfirmationBatchResultDto
        {
            TotalRequested = employeeIds.Count
        };

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var createdFiles = new List<string>();
        try
        {
            foreach (var employee in employees)
            {
                var confirmationDate = DateOnly.FromDateTime(DateTime.UtcNow);
                var generatedAt = DateTime.UtcNow;

                employee.Status = EmployeeStatusEnum.Active.ToString();
                employee.UpdatedBy = confirmedBy;
                employee.UpdatedOn = generatedAt;

                employee.Employment!.ConfirmationDate = confirmationDate;
                employee.Employment.UpdatedBy = confirmedBy;
                employee.Employment.UpdatedOn = generatedAt;

                var pdfBytes = BuildConfirmationLetterPdf(employee, confirmationDate, generatedAt, confirmedBy);
                var documentFolder = Path.Combine(AppContext.BaseDirectory, "EmployeeDocuments", "ConfirmationLetters", SanitizePathPart(employee.EmployeeCode));
                Directory.CreateDirectory(documentFolder);

                var fileName = $"ConfirmationLetter_{SanitizePathPart(employee.EmployeeCode)}_{generatedAt:yyyyMMddHHmmss}.pdf";
                var filePath = Path.Combine(documentFolder, fileName);
                await File.WriteAllBytesAsync(filePath, pdfBytes, cancellationToken);
                createdFiles.Add(filePath);

                var document = new HrmEmployeeDocument
                {
                    EmployeeId = employee.Id,
                    DocumentType = DocumentTypeEnum.ConfirmationLetter.ToString(),
                    DocumentNo = $"CONF-{employee.EmployeeCode}",
                    IssueDate = confirmationDate,
                    FileName = fileName,
                    FilePath = filePath,
                    CreatedBy = confirmedBy,
                    UpdatedBy = confirmedBy,
                    IsActive = true
                };

                _context.HrmEmployeeDocuments.Add(document);
                result.Items.Add(new ProbationConfirmationResultItemDto
                {
                    EmployeeId = employee.Id,
                    LetterDocumentId = document.Id,
                    DocumentPath = filePath,
                    ConfirmationDate = confirmationDate,
                    GeneratedAt = generatedAt,
                    ConfirmedBy = confirmedBy,
                    Succeeded = true
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);

            foreach (var file in createdFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch
                {
                    // best-effort cleanup
                }
            }

            throw;
        }

        result.SucceededCount = result.Items.Count(i => i.Succeeded);
        result.FailedCount = result.Items.Count(i => !i.Succeeded);
        return result;
    }

    private static string SanitizePathPart(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "unknown";

        foreach (var invalid in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(invalid, '_');
        }

        return value.Trim();
    }

    private static byte[] BuildConfirmationLetterPdf(HrmEmployeeMaster employee, DateOnly confirmationDate, DateTime generatedAt, string confirmedBy)
    {
        var lines = new List<string>
        {
            "CONFIRMATION LETTER",
            $"Employee: {employee.EmployeeName}",
            $"Employee ID: {employee.Id}",
            $"Employee Code: {employee.EmployeeCode}",
            $"Confirmation Date: {confirmationDate:yyyy-MM-dd}",
            $"Generated At: {generatedAt:yyyy-MM-dd HH:mm:ss} UTC",
            $"Confirmed By: {confirmedBy}"
        };

        var contentLines = new List<string>
        {
            "BT",
            "/F1 12 Tf",
            "50 760 Td"
        };

        for (var i = 0; i < lines.Count; i++)
        {
            var text = EscapePdfText(lines[i]);
            if (i == 0)
                contentLines.Add($"({text}) Tj");
            else
                contentLines.Add($"0 -24 Td ({text}) Tj");
        }

        contentLines.Add("ET");
        var content = string.Join("\n", contentLines);
        return BuildPdfDocument(content);
    }

    private static byte[] BuildPdfDocument(string content)
    {
        var objects = new List<string>
        {
            "1 0 obj<< /Type /Catalog /Pages 2 0 R >>endobj\n",
            "2 0 obj<< /Type /Pages /Kids [3 0 R] /Count 1 >>endobj\n",
            "3 0 obj<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>endobj\n",
            "4 0 obj<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>endobj\n"
        };

        var contentBytes = Encoding.ASCII.GetBytes(content);
        objects.Add($"5 0 obj<< /Length {contentBytes.Length} >>stream\n{content}\nendstream endobj\n");

        var header = "%PDF-1.4\n";
        var bytes = new List<byte>(Encoding.ASCII.GetBytes(header));
        var offsets = new List<int> { 0 };

        foreach (var obj in objects)
        {
            offsets.Add(bytes.Count);
            bytes.AddRange(Encoding.ASCII.GetBytes(obj));
        }

        var xrefStart = bytes.Count;
        var xref = new StringBuilder();
        xref.AppendLine("xref");
        xref.AppendLine($"0 {objects.Count + 1}");
        xref.AppendLine("0000000000 65535 f ");
        for (var i = 1; i < offsets.Count; i++)
        {
            xref.AppendLine($"{offsets[i]:0000000000} 00000 n ");
        }

        var trailer = new StringBuilder();
        trailer.AppendLine("trailer");
        trailer.AppendLine($"<< /Size {objects.Count + 1} /Root 1 0 R >>");
        trailer.AppendLine("startxref");
        trailer.AppendLine(xrefStart.ToString());
        trailer.AppendLine("%%EOF");

        bytes.AddRange(Encoding.ASCII.GetBytes(xref.ToString()));
        bytes.AddRange(Encoding.ASCII.GetBytes(trailer.ToString()));
        return bytes.ToArray();
    }

    private static string EscapePdfText(string text)
        => text.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
}
