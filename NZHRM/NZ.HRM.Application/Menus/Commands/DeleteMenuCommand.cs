// C#
using MediatR;

public record DeleteMenuCommand(string Id) : IRequest;
