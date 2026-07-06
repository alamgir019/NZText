using NZ.HRM.Application.UserRoles.Queries.GetAllUserRoles;
using NZ.HRM.Application.UserRoles.Queries.GetUserRoleById;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.SecUserRoles.Queries.GetUserRoleById;

namespace NZ.HRM.Application.UserRoles.Handlers;

public class UserRoleQueryHandler
{
    private readonly IUserRoleRepository _repository;

    public UserRoleQueryHandler(IUserRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserRoleDto>> Handle(GetAllUserRolesQuery query, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new UserRoleDto
        {
            Id = x.Id,
            UserId = x.UserId,
            RoleId = x.RoleId,
            EffectiveDate = x.EffectiveDate,
            ExpiryDate = x.ExpiryDate
        }).ToList();
    }

    public async Task<UserRoleDetailDto?> Handle(GetUserRoleByIdQuery query, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(query.Id, cancellationToken);
        if (item == null) return null;
        return new UserRoleDetailDto
        {
            Id = item.Id,
            UserId = item.UserId,
            RoleId = item.RoleId,
            EffectiveDate = item.EffectiveDate,
            ExpiryDate = item.ExpiryDate
        };
    }
}
