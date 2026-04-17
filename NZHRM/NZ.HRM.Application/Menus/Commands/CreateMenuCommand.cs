// C#
using MediatR;

public record CreateMenuCommand(string Name, int? ParentId, string? Url, string? Icon, int Order) : IRequest<string>;
