using NZ.HRM.Application.EmployeeMasters.Queries.GetAllEmployeeMasters;
using NZ.HRM.Application.EmployeeMasters.Queries.GetEmployeeMasterById;
using NZ.HRM.Application.EmployeeMasters.Queries.VerifyEmployeeCodeUniqueness;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.EmployeeMasters.Handlers;

public class EmployeeMasterQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public EmployeeMasterQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<List<EmployeeMasterDto>> Handle(GetAllEmployeeMastersQuery query, CancellationToken cancellationToken = default)
    {
        List<Domain.Entities.HrmEmployeeMaster> employees;

        if (!string.IsNullOrEmpty(query.CompanyId))
        {
            employees = await _employeeMasterRepository.GetByCompanyIdAsync(query.CompanyId, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(query.DepartmentId))
        {
            employees = await _employeeMasterRepository.GetByDepartmentIdAsync(query.DepartmentId, cancellationToken);
        }
        else
        {
            employees = await _employeeMasterRepository.GetAllAsync(includeInactive: query.IncludeInactive, cancellationToken: cancellationToken);
        }

        return employees.Select(e => new EmployeeMasterDto
        {
            Id = e.Id,
            EmployeeCode = e.EmployeeCode,
            EmployeeNameEnglish = e.EmployeeName,
            EmployeeNameBangla = e.EmployeeNameBangla,
            //CompanyId = e.CompanyId,
            //CompanyName = e.Company?.CompanyName ?? string.Empty,
            //DepartmentId = e.DepartmentId,
            //DepartmentName = e.Department?.DepartmentName ?? string.Empty,
            //SectionId = e.SectionId,
            //SectionName = e.Section?.SectionName ?? string.Empty,
            //GradeId = e.GradeId,
            //GradeName = e.Grade?.GradeName ?? string.Empty,
            //EmployeeType = e.EmployeeType,
            //ShiftId = e.ShiftId,
            //ShiftName = e.Shift?.ShiftName ?? string.Empty,
            //EmployeeNatureId = e.EmployeeNatureId,
            //EmployeeNatureName = e.EmployeeNature?.NatureName ?? string.Empty,
            //Holiday = e.Holiday,
            //ProposedMonthlySalary = e.ProposedMonthlySalary,
            //JoiningDate = e.JoiningDate,
            //ConfirmationDate = e.ConfirmationDate,
            //Status = e.Status,
            CreatedOn = e.CreatedOn,
            CreatedBy = e.CreatedBy,
            UpdatedOn = e.UpdatedOn,
            UpdatedBy = e.UpdatedBy,
            IsActive = e.IsActive
        }).ToList();
    }

    public async Task<EmployeeMasterDetailDto?> Handle(GetEmployeeMasterByIdQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.Id, cancellationToken);

        if (employee == null)
            return null;

        return new EmployeeMasterDetailDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            EmployeeNameEnglish = employee.EmployeeName,
            EmployeeNameBangla = employee.EmployeeNameBangla,
            //CompanyId = employee.CompanyId,
            //CompanyName = employee.Company?.CompanyName ?? string.Empty,
            //DepartmentId = employee.DepartmentId,
            //DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
            //SectionId = employee.SectionId,
            //SectionName = employee.Section?.SectionName ?? string.Empty,
            //GradeId = employee.GradeId,
            //GradeName = employee.Grade?.GradeName ?? string.Empty,
            //EmployeeType = employee.EmployeeType,
            //ShiftId = employee.ShiftId,
            //ShiftName = employee.Shift?.ShiftName ?? string.Empty,
            //EmployeeNatureId = employee.EmployeeNatureId,
            //EmployeeNatureName = employee.EmployeeNature?.NatureName ?? string.Empty,
            //Holiday = employee.Holiday,
            //ProposedMonthlySalary = employee.ProposedMonthlySalary,
            //JoiningDate = employee.JoiningDate,
            //ConfirmationDate = employee.ConfirmationDate,
            //Status = employee.Status,
            CreatedOn = employee.CreatedOn,
            CreatedBy = employee.CreatedBy,
            UpdatedOn = employee.UpdatedOn,
            UpdatedBy = employee.UpdatedBy,
            IsActive = employee.IsActive
        };
    }

    public async Task<EmployeeCodeUniquenessDto> Handle(VerifyEmployeeCodeUniquenessQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.EmployeeCode))
        {
            return new EmployeeCodeUniquenessDto
            {
                IsUnique = false,
                Message = "Employee code cannot be empty"
            };
        }

        // Check uniqueness at database level - efficient single query
        var isUnique = await _employeeMasterRepository.IsEmployeeCodeUniqueAsync(
            query.EmployeeCode,
            cancellationToken);

        return new EmployeeCodeUniquenessDto
        {
            IsUnique = isUnique,
            Message = isUnique 
                ? "Employee code is available" 
                : $"Employee code '{query.EmployeeCode}' is already in use"
        };
    }
}
