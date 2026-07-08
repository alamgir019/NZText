using NZ.HRM.Application.GroupComplexes.Commands.CreateGroupComplex;
using NZ.HRM.Application.GroupComplexes.Commands.DeleteGroupComplex;
using NZ.HRM.Application.GroupComplexes.Commands.UpdateGroupComplex;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.GroupComplexes.Handlers;

public class GroupComplexCommandHandler
{
    private readonly IGroupComplexRepository _groupComplexRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupComplexCommandHandler(IGroupComplexRepository groupComplexRepository, IGroupRepository groupRepository)
    {
        _groupComplexRepository = groupComplexRepository;
        _groupRepository = groupRepository;
    }

    public async Task<string> Handle(CreateGroupComplexCommand command, CancellationToken cancellationToken = default)
    {
        // validate group
        var groupExists = await _groupRepository.ExistsAsync(command.GroupId, cancellationToken);
        if (!groupExists) throw new KeyNotFoundException($"Group with ID {command.GroupId} not found");

        var gc = new MstGroupComplex
        {
            GroupId = command.GroupId,
            ComplexCode = command.ComplexCode,
            ComplexName = command.ComplexName,
            IsActive = true
        };

        return await _groupComplexRepository.AddAsync(gc, cancellationToken);
    }

    public async Task Handle(UpdateGroupComplexCommand command, CancellationToken cancellationToken = default)
    {
        var gc = await _groupComplexRepository.GetByIdAsync(command.Id, cancellationToken);
        if (gc == null) throw new KeyNotFoundException($"GroupComplex with ID {command.Id} not found");

        var groupExists = await _groupRepository.ExistsAsync(command.GroupId, cancellationToken);
        if (!groupExists) throw new KeyNotFoundException($"Group with ID {command.GroupId} not found");

        gc.GroupId = command.GroupId;
        gc.ComplexCode = command.ComplexCode;
        gc.ComplexName = command.ComplexName;

        await _groupComplexRepository.UpdateAsync(gc, cancellationToken);
    }

    public async Task Handle(DeleteGroupComplexCommand command, CancellationToken cancellationToken = default)
    {
        var gc = await _groupComplexRepository.GetByIdAsync(command.Id, cancellationToken);
        if (gc == null) throw new KeyNotFoundException($"GroupComplex with ID {command.Id} not found");

        gc.IsActive = false;
        await _groupComplexRepository.UpdateAsync(gc, cancellationToken);
    }
}
