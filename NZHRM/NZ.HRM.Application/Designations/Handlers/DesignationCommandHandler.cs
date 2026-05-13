using NZ.HRM.Application.Designations.Commands.CreateDesignation;
using NZ.HRM.Application.Designations.Commands.DeleteDesignation;
using NZ.HRM.Application.Designations.Commands.UpdateDesignation;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Designations.Handlers;

public class DesignationCommandHandler
{
    private readonly IDesignationRepository _designationRepository;

    public DesignationCommandHandler(IDesignationRepository designationRepository)
    {
        _designationRepository = designationRepository;
    }

    public async Task<string> Handle(CreateDesignationCommand command, CancellationToken cancellationToken = default)
    {
        var designation = new Designation
        {
            DesignationName = command.DesignationName,
            DesignationCode = command.DesignationCode ?? string.Empty,
            ParentId = command.ParentId ?? string.Empty,
            IsActive = true
        };

        return await _designationRepository.AddAsync(designation, cancellationToken);
    }

    public async Task Handle(UpdateDesignationCommand command, CancellationToken cancellationToken = default)
    {
        var designation = await _designationRepository.GetByIdAsync(command.Id, cancellationToken);
        if (designation == null)
            throw new KeyNotFoundException($"Designation with ID {command.Id} not found");

        designation.DesignationName = command.DesignationName;
        designation.DesignationCode = command.DesignationCode ?? string.Empty;
        designation.ParentId = command.ParentId ?? string.Empty;

        await _designationRepository.UpdateAsync(designation, cancellationToken);
    }

    public async Task Handle(DeleteDesignationCommand command, CancellationToken cancellationToken = default)
    {
        var designation = await _designationRepository.GetByIdAsync(command.Id, cancellationToken);
        if (designation == null)
            throw new KeyNotFoundException($"Designation with ID {command.Id} not found");

        designation.IsActive = false;
        await _designationRepository.UpdateAsync(designation, cancellationToken);
    }
}
