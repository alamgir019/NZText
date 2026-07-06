using NZ.HRM.Application.Permissions.Queries.GetAllSecPermissions;
using NZ.HRM.Application.Permissions.Queries.GetSecPermissionById;
using NZ.HRM.Application.Permissions.Queries.GetAllPermissions;

namespace NZ.HRM.Application.Permissions.Handlers;


public class PermissionQueryHandler
{
    private readonly IMenuPermissionRepository _repository;

    public PermissionQueryHandler(IMenuPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SecPermissionDto>> Handle(GetAllPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync();
        return items.Select(x => new SecPermissionDto
        {
            Id = x.Id,
            PermissionCode = x.PermissionCode,
            PermissionName = x.PermissionName,
            ModuleName = x.ModuleName,
            PermissionType = x.PermissionType?.ToString()
        }).ToList();
    }

    public async Task<SecPermissionDetailDto?> Handle(GetPermissionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var item = await _repository.FindByIdAsync(query.Id);
        if (item == null) return null;
        return new SecPermissionDetailDto
        {
            Id = item.Id,
            PermissionCode = item.PermissionCode,
            PermissionName = item.PermissionName,
            ModuleName = item.ModuleName,
            PermissionType = item.PermissionType?.ToString()
        };
    }
}
