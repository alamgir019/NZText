using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.CreatePhysicalExaminationSetting;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.DeletePhysicalExaminationSetting;
using NZ.HRM.Application.PhysicalExaminationSettings.Commands.UpdatePhysicalExaminationSetting;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.PhysicalExaminationSettings.Handlers;

public class PhysicalExaminationSettingCommandHandler
{
    private readonly IPhysicalExaminationSettingRepository _repository;

    public PhysicalExaminationSettingCommandHandler(IPhysicalExaminationSettingRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreatePhysicalExaminationSettingCommand command, CancellationToken cancellationToken = default)
    {
        var setting = new PhysicalExaminationSetting
        {
            FieldName = command.FieldName,
            DisplayOrder = command.DisplayOrder,
            IsBinaryCheck = command.IsBinaryCheck,
            AllowRemarks = command.AllowRemarks,
            IsActive = true
        };

        return await _repository.AddAsync(setting, cancellationToken);
    }

    public async Task Handle(UpdatePhysicalExaminationSettingCommand command, CancellationToken cancellationToken = default)
    {
        var setting = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (setting == null)
            throw new KeyNotFoundException($"Physical examination setting with ID {command.Id} not found");

        setting.FieldName = command.FieldName;
        setting.DisplayOrder = command.DisplayOrder;
        setting.IsBinaryCheck = command.IsBinaryCheck;
        setting.AllowRemarks = command.AllowRemarks;

        await _repository.UpdateAsync(setting, cancellationToken);
    }

    public async Task Handle(DeletePhysicalExaminationSettingCommand command, CancellationToken cancellationToken = default)
    {
        var setting = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (setting == null)
            throw new KeyNotFoundException($"Physical examination setting with ID {command.Id} not found");

        setting.IsActive = false;
        await _repository.UpdateAsync(setting, cancellationToken);
    }
}
