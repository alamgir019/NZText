using NZ.HRM.Application.Employees.Queries.GetEmployeeDetailForIT;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Handlers;

public class EmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public EmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeDetailForIT?> Handle(GetEmployeeDetailForITQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var dto = employee.MapToEmployeeDetailForIT();

        return dto;
    }

    public async Task<Queries.GetItActivationSummary.ItActivationSummaryDto> Handle(Queries.GetItActivationSummary.GetItActivationSummaryQuery query, CancellationToken cancellationToken = default)
    {
        // Fetch active employees up to now and filter by status
        var itActivated = await _employeeMasterRepository.GetByStatusUpToDateAsync(query, cancellationToken: cancellationToken);

        var dto = new Queries.GetItActivationSummary.ItActivationSummaryDto
        {
            Total = itActivated.Count,
            Workers = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Worker.ToString(), StringComparison.OrdinalIgnoreCase)),
            Staff = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Staff.ToString(), StringComparison.OrdinalIgnoreCase)),
            Management = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Management.ToString(), StringComparison.OrdinalIgnoreCase))
        };
        var groupedByCompany = itActivated.GroupBy(e => e.Employment?.Unit?.UnitName);
        foreach (var group in groupedByCompany)
        {
            var companySummary = new Queries.GetItActivationSummary.ItActivationSummaryDto
            {
                CompanyName = group.Key,
                Total = group.Count(),
                Workers = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Worker.ToString(), StringComparison.OrdinalIgnoreCase)),
                Staff = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Staff.ToString(), StringComparison.OrdinalIgnoreCase)),
                Management = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Management.ToString(), StringComparison.OrdinalIgnoreCase))
            };
            dto.CompanySummaries.Add(companySummary);
        }
        return dto;
    }
}
