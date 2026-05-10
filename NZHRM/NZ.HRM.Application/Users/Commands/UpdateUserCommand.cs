public record UpdateUserCommand(string Id, string Username, string Password, string RoleId, string UpdatedBy, bool IsActive);
