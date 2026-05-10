using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Sections.Commands.CreateSection;
using NZ.HRM.Application.Sections.Commands.DeleteSection;
using NZ.HRM.Application.Sections.Commands.UpdateSection;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Sections.Handlers;

public class SectionCommandHandler
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public SectionCommandHandler(
        ISectionRepository sectionRepository,
        IDepartmentRepository departmentRepository)
    {
        _sectionRepository = sectionRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<string> Handle(CreateSectionCommand command, CancellationToken cancellationToken = default)
    {
        // Validate that department exists
        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
        {
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");
        }

        var section = new Section
        {
            DepartmentId = command.DepartmentId,
            SectionName = command.SectionName,
            IsActive = true
        };

        return await _sectionRepository.AddAsync(section, cancellationToken);
    }

    public async Task Handle(UpdateSectionCommand command, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (section == null)
            throw new KeyNotFoundException($"Section with ID {command.Id} not found");

        // Validate that department exists
        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
        {
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");
        }

        section.DepartmentId = command.DepartmentId;
        section.SectionName = command.SectionName;

        await _sectionRepository.UpdateAsync(section, cancellationToken);
    }

    public async Task Handle(DeleteSectionCommand command, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (section == null)
            throw new KeyNotFoundException($"Section with ID {command.Id} not found");

        // Soft delete
        section.IsActive = false;
        await _sectionRepository.UpdateAsync(section, cancellationToken);

        // Or hard delete
        // await _sectionRepository.DeleteAsync(section, cancellationToken);
    }
}
