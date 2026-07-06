using NZ.HRM.Application.UserRoles.Commands.DeleteUserRole;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Application.SecUserRoles.Commands.UpdateUserRole;
using NZ.HRM.Application.SecUserRoles.Commands.CreateUserRole;

namespace NZ.HRM.Application.UserRoles.Handlers;

public class UserRoleCommandHandler
{
    private readonly IUserRoleRepository _repository;

    public UserRoleCommandHandler(IUserRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new SecUserRole
        {
            UserId = command.UserId,
            RoleId = command.RoleId,
            EffectiveDate = command.EffectiveDate,
            ExpiryDate = command.ExpiryDate
        };

        return await _repository.AddAsync(entity, cancellationToken);
    }

    public async Task Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"UserRole with ID {command.Id} not found");

        entity.UserId = command.UserId;
        entity.RoleId = command.RoleId;
        entity.EffectiveDate = command.EffectiveDate;
        entity.ExpiryDate = command.ExpiryDate;

        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task Handle(DeleteUserRoleCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"SecUserRole with ID {command.Id} not found");

        await _repository.DeleteAsync(entity, cancellationToken);
    }
}
