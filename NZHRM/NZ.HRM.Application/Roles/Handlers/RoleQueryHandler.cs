using NZ.HRM.Domain.Entities;

public class RoleQueryHandler
{
    private readonly IRoleRepository _repo;
    public RoleQueryHandler(IRoleRepository repo) => _repo = repo;

    public async Task<Role?> Handle(GetRoleByIdQuery query)
        => await _repo.FindByIdAsync(query.Id);

    public async Task<List<Role>> Handle(GetAllRolesQuery query)
        => await _repo.GetAllAsync();
}
