// C#
using MediatR;
using NZ.HRM.Domain.Entities;

public record GetMenuByIdQuery(string Id) : IRequest<Menu?>;
