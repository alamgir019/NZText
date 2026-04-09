using NZ.HRM.Domain.Entities;

public class UserQueryHandler
{
    private readonly IUserRepository _repo;
    public UserQueryHandler(IUserRepository repo) => _repo = repo;

    public async Task<User?> Handle(GetUserByIdQuery query)
        => await _repo.FindByIdAsync(query.Id);

    public async Task<List<User>> Handle(GetAllUsersQuery query)
        => await _repo.GetAllAsync();
}
