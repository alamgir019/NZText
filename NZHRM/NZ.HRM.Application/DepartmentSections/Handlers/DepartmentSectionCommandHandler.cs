using NZ.HRM.Application.DepartmentSections.Commands.CreateDepartmentSection;
using NZ.HRM.Application.DepartmentSections.Commands.DeleteDepartmentSection;
using NZ.HRM.Application.DepartmentSections.Commands.UpdateDepartmentSection;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.DepartmentSections.Handlers;

public class DepartmentSectionCommandHandler
{
    private readonly IDepartmentSectionRepository _departmentSectionRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISectionRepository _sectionRepository;

    public DepartmentSectionCommandHandler(
        IDepartmentSectionRepository departmentSectionRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository)
    {
        _departmentSectionRepository = departmentSectionRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
    }

    public async Task<string> Handle(CreateDepartmentSectionCommand command, CancellationToken cancellationToken = default)
    {
        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        var departmentSection = new MstDepartmentSection
        {
            DepartmentId = command.DepartmentId,
            SectionId = command.SectionId,
            IsActive = true
        };

        return await _departmentSectionRepository.AddAsync(departmentSection, cancellationToken);
    }

    public async Task Handle(UpdateDepartmentSectionCommand command, CancellationToken cancellationToken = default)
    {
        var departmentSection = await _departmentSectionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (departmentSection == null)
            throw new KeyNotFoundException($"DepartmentSection with ID {command.Id} not found");

        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        departmentSection.DepartmentId = command.DepartmentId;
        departmentSection.SectionId = command.SectionId;

        await _departmentSectionRepository.UpdateAsync(departmentSection, cancellationToken);
    }

    public async Task Handle(DeleteDepartmentSectionCommand command, CancellationToken cancellationToken = default)
    {
        var departmentSection = await _departmentSectionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (departmentSection == null)
            throw new KeyNotFoundException($"DepartmentSection with ID {command.Id} not found");

        departmentSection.IsActive = false;
        await _departmentSectionRepository.UpdateAsync(departmentSection, cancellationToken);
    }
}
