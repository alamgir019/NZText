using NZ.HRM.Application.EmployeeNatures.Queries.GetAllEmployeeNatures;
using NZ.HRM.Application.EmployeeNatures.Queries.GetEmployeeNatureById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.EmployeeNatures.Handlers;

public class EmployeeNatureQueryHandler
{
    private readonly IEmployeeNatureRepository _employeeNatureRepository;

    public EmployeeNatureQueryHandler(IEmployeeNatureRepository employeeNatureRepository)
    {
        _employeeNatureRepository = employeeNatureRepository;
    }

    public async Task<List<EmployeeNatureDto>> Handle(GetAllEmployeeNaturesQuery query, CancellationToken cancellationToken = default)
    {
        var natures = await _employeeNatureRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return natures.Select(n => new EmployeeNatureDto
        {
            Id = n.Id,
            NatureName = n.NatureName,
            SortOrder = n.SortOrder,
            CreatedOn = n.CreatedOn,
            CreatedBy = n.CreatedBy,
            UpdatedOn = n.UpdatedOn,
            UpdatedBy = n.UpdatedBy,
            IsActive = n.IsActive
        }).ToList();
    }

    public async Task<EmployeeNatureDetailDto?> Handle(GetEmployeeNatureByIdQuery query, CancellationToken cancellationToken = default)
    {
        var nature = await _employeeNatureRepository.GetByIdAsync(query.Id, cancellationToken);
        if (nature == null)
            return null;

        return new EmployeeNatureDetailDto
        {
            Id = nature.Id,
            NatureName = nature.NatureName,
            SortOrder = nature.SortOrder,
            CreatedOn = nature.CreatedOn,
            CreatedBy = nature.CreatedBy,
            UpdatedOn = nature.UpdatedOn,
            UpdatedBy = nature.UpdatedBy,
            IsActive = nature.IsActive
        };
    }
}
