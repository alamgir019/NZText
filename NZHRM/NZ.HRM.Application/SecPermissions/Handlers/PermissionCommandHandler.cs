using NZ.HRM.Application.Permissions.Commands.CreatePermission;
using NZ.HRM.Application.Permissions.Commands.UpdatePermission;
using NZ.HRM.Application.Permissions.Commands.DeletePermission;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Permissions.Handlers;

public class PermissionCommandHandler
{
    private readonly IMenuPermissionRepository _repository;

    public PermissionCommandHandler(IMenuPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new SecPermission
        {
            PermissionCode = command.PermissionCode,
            PermissionName = command.PermissionName,
            ModuleName = command.ModuleName
        };

        if (!string.IsNullOrWhiteSpace(command.PermissionType) &&
            Enum.TryParse<PermissionType>(command.PermissionType, true, out var parsed))
        {
            entity.PermissionType = parsed;
        }

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Handle(UpdatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FindByIdAsync(command.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Permission with ID {command.Id} not found");

        entity.PermissionCode = command.PermissionCode;
        entity.PermissionName = command.PermissionName;
        entity.ModuleName = command.ModuleName;
        if (!string.IsNullOrWhiteSpace(command.PermissionType) &&
            Enum.TryParse<PermissionType>(command.PermissionType, true, out var parsed2))
        {
            entity.PermissionType = parsed2;
        }

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task Handle(DeletePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FindByIdAsync(command.Id);
        if (entity == null)
            throw new KeyNotFoundException($"Permission with ID {command.Id} not found");

        await _repository.RemoveAsync(entity);
        await _repository.SaveChangesAsync();
    }
}
