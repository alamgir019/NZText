using NZ.HRM.Application.GroupComplexes.Queries.GetAllGroupComplexes;
using NZ.HRM.Application.GroupComplexes.Queries.GetGroupComplexById;
using NZ.HRM.Application.Interfaces.Repositories;
using System.Linq;

namespace NZ.HRM.Application.GroupComplexes.Handlers;

public class GroupComplexQueryHandler
{
    private readonly IGroupComplexRepository _groupComplexRepository;

    public GroupComplexQueryHandler(IGroupComplexRepository groupComplexRepository)
    {
        _groupComplexRepository = groupComplexRepository;
    }

    public async Task<List<GroupComplexDto>> Handle(GetAllGroupComplexesQuery query, CancellationToken cancellationToken = default)
    {
        var list = await _groupComplexRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        return list.Select(g => new GroupComplexDto
        {
            Id = g.Id,
            GroupId = g.GroupId,
            ComplexCode = g.ComplexCode,
            ComplexName = g.ComplexName
        }).ToList();
    }

    public async Task<GroupComplexDetailDto?> Handle(GetGroupComplexByIdQuery query, CancellationToken cancellationToken = default)
    {
        var g = await _groupComplexRepository.GetByIdAsync(query.Id, cancellationToken);
        if (g == null) return null;
        return new GroupComplexDetailDto
        {
            Id = g.Id,
            GroupId = g.GroupId,
            ComplexCode = g.ComplexCode,
            ComplexName = g.ComplexName,
            IsActive = g.IsActive
        };
    }
}
