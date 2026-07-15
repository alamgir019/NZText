using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetail;
using NZ.HRM.Application.Employees.Queries.GetEmployeesByStatus;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDocuments;

namespace NZ.HRM.Application.Employees.Handlers;

public class CompleteEmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;
    private readonly IEmployeeDocumentRepository _employeeDocumentRepository;

    public CompleteEmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository, IEmployeeDocumentRepository employeeDocumentRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _employeeDocumentRepository = employeeDocumentRepository;
    }

    public async Task<List<EmployeeDocumentDto>> Handle(GetEmployeeDocumentsQuery query, CancellationToken cancellationToken = default)
    {
        var docs = await _employeeDocumentRepository.GetByEmployeeIdAsync(query.EmployeeId, cancellationToken);
        if (docs == null || !docs.Any())
            return new List<EmployeeDocumentDto>();

        return docs.Select(d => new EmployeeDocumentDto
        {
            EmployeeId = d.EmployeeId,
            DocumentType = !string.IsNullOrWhiteSpace(d.DocumentType) && Enum.TryParse<NZ.HRM.Utility.Enum.DocumentType>(d.DocumentType, out var dt) ? dt : null,
            DocumentNo = d.DocumentNo,
            IssueDate = d.IssueDate,
            ExpiryDate = d.ExpiryDate,
            FileName = d.FileName,
            FilePath = d.FilePath
        }).ToList();
    }

    public async Task<EmployeeDetailDto?> Handle(GetEmployeeDetailQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);

        if (employee == null)
            return null;

        return employee.MapToEmployeeDetailDto();
    }

    public async Task<HrmEmployeeDocument?> Handle(string employeeId, CancellationToken cancellationToken = default)
    {
        var documents = await _employeeDocumentRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
        if (documents == null)
            return null;
        var photoDocument = documents.LastOrDefault(d => d.DocumentType == "Photo");
        return photoDocument;
    }
    
    public async Task<List<EmployeeSearchDto>> Handle(SearchEmployeesQuery query, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeMasterRepository.SearchAsync(query.SearchText, cancellationToken);

        return employees.Select(x => 
        new EmployeeSearchDto() {
            Id = x.Id,
            EmployeeNameEnglish = x.EmployeeName,
            EmployeeNameBangla = x.EmployeeNameBangla,
            EnrollmentId = x.EnrollmentId,
        }).ToList();
    }

    public async Task<DateTime?> Handle(GetEmployeeConfirmationDateQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        //var probationMonths = employee.EmployeeType == EmployeeType.Worker ? 3 : 6;
        //return query.JoiningDate.AddMonths(probationMonths).Date;
        return null;
    }

    public async Task<List<EmployeeByStatusDto>> Handle(GetEmployeesByStatusQuery query, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeMasterRepository.GetByStatusAsync(query.Status, cancellationToken);

        return employees.Select(x => new EmployeeByStatusDto
        {
            EmployeeId = x.Id,
            EnrollmentId = x.EnrollmentId,
            EmployeeName = string.IsNullOrWhiteSpace(x.EmployeeName) ? x.EmployeeNameBangla : x.EmployeeName,
            Age = CalculateAge(x.Personal?.DateOfBirth),
            ExaminationDate = x.MedicalFitnessCheck?.ExaminationDateTime,
            MedicalResult = !string.IsNullOrWhiteSpace(x.MedicalFitnessCheck?.Fitness) && Enum.TryParse<FitnessOption>(x.MedicalFitnessCheck.Fitness, out var fitnessOption)
                ? fitnessOption
                : null,            
            DateOfJoining = x.Employment?.JoiningDate,
            DateOfBirth = x.Personal?.DateOfBirth,
            BloodGroup = !string.IsNullOrWhiteSpace(x.Personal?.BloodGroup) && Enum.TryParse<BloodGroup>(x.Personal.BloodGroup, out var bloodGroup)
                ? bloodGroup
                : null,
            Gender = !string.IsNullOrWhiteSpace(x.Personal?.Gender) && Enum.TryParse<Gender>(x.Personal.Gender, out var gender)
                ? gender
                : null,
            Department = x.Employment?.Department?.DepartmentName,
            Section = x.Employment?.Section?.SectionName,
            Cell = x.Employment?.Cell?.CellName,
            Designation = x.Employment?.Designation?.DesignationName,
            Grade = x.Employment?.Grade?.GradeName,
            Shift = x.Employment?.Shift?.ShiftName,
            WeekOffDay = !string.IsNullOrWhiteSpace(x.Employment?.WeeklyOffDay) && Enum.TryParse<WeekOffDay>(x.Employment.WeeklyOffDay, out var weekOffDay)
                ? weekOffDay
                : null, 
            ProposedSalary = x.Payroll?.ProposedSalary
        }).ToList();
    }

    private static int? CalculateAge(DateOnly? dateOfBirth)
    {
        if (!dateOfBirth.HasValue)
        {
            return null;
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Value.Year;

        if (dateOfBirth.Value > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
