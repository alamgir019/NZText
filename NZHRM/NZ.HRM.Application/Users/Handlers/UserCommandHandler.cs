using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Helper;

public class UserCommandHandler
{
    private readonly IUserRepository _repo;
    public UserCommandHandler(IUserRepository repo) => _repo = repo;

    public async Task<string> Handle(CreateUserCommand cmd)
    {
        var user = new SecUser
        {
            Id = IdentityGenerator.Next(),
            UserName = cmd.Username,
            PasswordHash = cmd.Password, // Hash in production!
            Role = cmd.Role,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = cmd.CreatedBy,
            UpdatedOn = DateTime.UtcNow,
            UpdatedBy = cmd.CreatedBy,
            IsActive = true
        };

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();
        return user.Id;
    }

    public async Task Handle(UpdateUserCommand cmd)
    {
        var user = await _repo.FindByIdAsync(cmd.Id);
        if (user is null) throw new Exception("User not found");
        //user.Username = cmd.Username;
        //user.Password = cmd.Password; // Hash in production!
        //user.RoleId = cmd.RoleId;
        user.UpdatedOn = DateTime.UtcNow;
        user.UpdatedBy = cmd.UpdatedBy;
        user.IsActive = cmd.IsActive;
        await _repo.UpdateAsync(user);
        await _repo.SaveChangesAsync();
    }

    public async Task Handle(DeleteUserCommand cmd)
    {
        var user = await _repo.FindByIdAsync(cmd.Id);
        if (user is null) throw new Exception("User not found");
        await _repo.RemoveAsync(user);
        await _repo.SaveChangesAsync();
    }

    public async Task<SecUser?> Handle(LoginUserCommand cmd)
    {
        var user = await _repo.FindByUsernameAsync(cmd.Username);
        if (user == null) return null;
        // In production, use hashed and salted password comparison!
        if (user.PasswordHash != cmd.Password) return null;
        return user;
    }

}
