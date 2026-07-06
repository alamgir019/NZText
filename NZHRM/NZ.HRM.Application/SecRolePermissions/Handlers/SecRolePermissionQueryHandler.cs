using NZ.HRM.Application.RolePermissions.Queries.GetAllRolePermissions;
using NZ.HRM.Application.RolePermissions.Queries.GetRolePermissionById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.RolePermissions.Handlers;

public class RolePermissionQueryHandler
{
    private readonly IRolePermissionRepository _repository;

    public RolePermissionQueryHandler(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RolePermissionDto>> Handle(GetAllRolePermissionsQuery query, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return items.Select(x => new RolePermissionDto
        {
            Id = x.Id,
            RoleId = x.RoleId,
            PermissionId = x.PermissionId
        }).ToList();
    }

    public async Task<RolePermissionDetailDto?> Handle(GetRolePermissionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(query.Id, cancellationToken);
        if (item == null) return null;
        return new RolePermissionDetailDto
        {
            Id = item.Id,
            RoleId = item.RoleId,
            PermissionId = item.PermissionId
        };
    }
}
