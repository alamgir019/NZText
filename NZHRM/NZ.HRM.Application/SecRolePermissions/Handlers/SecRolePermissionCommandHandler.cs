using NZ.HRM.Application.RolePermissions.Commands.CreateRolePermission;
using NZ.HRM.Application.RolePermissions.Commands.DeleteRolePermission;
using NZ.HRM.Application.RolePermissions.Commands.UpdateRolePermission;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.RolePermissions.Handlers;

public class RolePermissionCommandHandler
{
    private readonly IRolePermissionRepository _repository;

    public RolePermissionCommandHandler(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateRolePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new SecRolePermission
        {
            RoleId = command.RoleId,
            PermissionId = command.PermissionId
        };

        return await _repository.AddAsync(entity, cancellationToken);
    }

    public async Task Handle(UpdateRolePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"RolePermission with ID {command.Id} not found");

        entity.RoleId = command.RoleId;
        entity.PermissionId = command.PermissionId;

        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task Handle(DeleteRolePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"RolePermission with ID {command.Id} not found");

        await _repository.DeleteAsync(entity, cancellationToken);
    }
}
