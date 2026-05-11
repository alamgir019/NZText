using NZ.HRM.Application.EmployeeMasters.Queries.GetAllEmployeeMasters;
using NZ.HRM.Application.EmployeeMasters.Queries.GetEmployeeMasterById;
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
        List<NZ.HRM.Domain.Entities.EmployeeMaster> employees;

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
            EmployeeNameEnglish = e.EmployeeNameEnglish,
            EmployeeNameBangla = e.EmployeeNameBangla,
            CompanyId = e.CompanyId,
            CompanyName = e.Company?.CompanyName ?? string.Empty,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.DepartmentName ?? string.Empty,
            SectionId = e.SectionId,
            SectionName = e.Section?.SectionName ?? string.Empty,
            GradeId = e.GradeId,
            GradeName = e.Grade?.GradeName ?? string.Empty,
            EmployeeType = e.EmployeeType,
            Shift = e.Shift,
            EmployeeNature = e.EmployeeNature,
            Holiday = e.Holiday,
            ProposedMonthlySalary = e.ProposedMonthlySalary,
            JoiningDate = e.JoiningDate,
            ConfirmationDate = e.ConfirmationDate,
            Status = e.Status,
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
            EmployeeNameEnglish = employee.EmployeeNameEnglish,
            EmployeeNameBangla = employee.EmployeeNameBangla,
            CompanyId = employee.CompanyId,
            CompanyName = employee.Company?.CompanyName ?? string.Empty,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
            SectionId = employee.SectionId,
            SectionName = employee.Section?.SectionName ?? string.Empty,
            GradeId = employee.GradeId,
            GradeName = employee.Grade?.GradeName ?? string.Empty,
            EmployeeType = employee.EmployeeType,
            Shift = employee.Shift,
            EmployeeNature = employee.EmployeeNature,
            Holiday = employee.Holiday,
            ProposedMonthlySalary = employee.ProposedMonthlySalary,
            JoiningDate = employee.JoiningDate,
            ConfirmationDate = employee.ConfirmationDate,
            Status = employee.Status,
            CreatedOn = employee.CreatedOn,
            CreatedBy = employee.CreatedBy,
            UpdatedOn = employee.UpdatedOn,
            UpdatedBy = employee.UpdatedBy,
            IsActive = employee.IsActive
        };
    }
}
