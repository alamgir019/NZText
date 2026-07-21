using NZ.HRM.Application.Designations.Queries.GetAllDesignations;
using NZ.HRM.Application.Designations.Queries.GetDesignationById;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Designations.Handlers;

public class DesignationQueryHandler
{
    private readonly IDesignationRepository _designationRepository;

    public DesignationQueryHandler(IDesignationRepository designationRepository)
    {
        _designationRepository = designationRepository;
    }

    public async Task<List<DesignationDto>> Handle(GetAllDesignationsQuery query, CancellationToken cancellationToken = default)
    {
        var designations = await _designationRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return designations.Select(static d => new DesignationDto
        {
            Id = d.Id,
            DesignationName = d.DesignationName,
            DesignationCode = d.DesignationCode,
            DesignationNameBangla = d.DesignationNameBangla,
            EmployeeNature = d.EmployeeNature != null && Enum.TryParse<EmployeeNature>(d.EmployeeNature, out var nature) ? nature : null,
            CreatedOn = d.CreatedOn,
            CreatedBy = d.CreatedBy,
            UpdatedOn = d.UpdatedOn,
            UpdatedBy = d.UpdatedBy,
            IsActive = d.IsActive
        }).ToList();
    }

    public async Task<DesignationDetailDto?> Handle(GetDesignationByIdQuery query, CancellationToken cancellationToken = default)
    {
        var designation = await _designationRepository.GetByIdAsync(query.Id, cancellationToken);
        if (designation == null) return null;

        return new DesignationDetailDto
        {
            Id = designation.Id,
            DesignationName = designation.DesignationName,
            DesignationCode = designation.DesignationCode,
            EmployeeNature = designation.EmployeeNature != null && Enum.TryParse<EmployeeNature>(designation.EmployeeNature, out var nature) ? nature : null,
            CreatedOn = designation.CreatedOn,
            CreatedBy = designation.CreatedBy,
            UpdatedOn = designation.UpdatedOn,
            UpdatedBy = designation.UpdatedBy,
            IsActive = designation.IsActive
        };
    }
}
