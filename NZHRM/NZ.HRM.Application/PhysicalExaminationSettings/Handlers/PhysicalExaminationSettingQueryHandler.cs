using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.PhysicalExaminationSettings.Queries.GetAllPhysicalExaminationSettings;
using NZ.HRM.Application.PhysicalExaminationSettings.Queries.GetPhysicalExaminationSettingById;

namespace NZ.HRM.Application.PhysicalExaminationSettings.Handlers;

public class PhysicalExaminationSettingQueryHandler
{
    private readonly IPhysicalExaminationSettingRepository _repository;

    public PhysicalExaminationSettingQueryHandler(IPhysicalExaminationSettingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PhysicalExaminationSettingDto>> Handle(GetAllPhysicalExaminationSettingsQuery query, CancellationToken cancellationToken = default)
    {
        var settings = await _repository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return settings.Select(x => new PhysicalExaminationSettingDto
        {
            Id = x.Id,
            FieldName = x.FieldName,
            DisplayOrder = x.DisplayOrder,
            IsBinaryCheck = x.IsBinaryCheck,
            AllowRemarks = x.AllowRemarks,
            CreatedOn = x.CreatedOn,
            CreatedBy = x.CreatedBy,
            UpdatedOn = x.UpdatedOn,
            UpdatedBy = x.UpdatedBy,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<PhysicalExaminationSettingDetailDto?> Handle(GetPhysicalExaminationSettingByIdQuery query, CancellationToken cancellationToken = default)
    {
        var setting = await _repository.GetByIdAsync(query.Id, cancellationToken);
        if (setting == null)
            return null;

        return new PhysicalExaminationSettingDetailDto
        {
            Id = setting.Id,
            FieldName = setting.FieldName,
            DisplayOrder = setting.DisplayOrder,
            IsBinaryCheck = setting.IsBinaryCheck,
            AllowRemarks = setting.AllowRemarks,
            CreatedOn = setting.CreatedOn,
            CreatedBy = setting.CreatedBy,
            UpdatedOn = setting.UpdatedOn,
            UpdatedBy = setting.UpdatedBy,
            IsActive = setting.IsActive
        };
    }
}
