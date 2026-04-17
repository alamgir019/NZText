// C#
using MediatR;

public record UpdateMenuCommand(string Id, string Name, int? ParentId, string? Url, string? Icon, int Order) : IRequest;
