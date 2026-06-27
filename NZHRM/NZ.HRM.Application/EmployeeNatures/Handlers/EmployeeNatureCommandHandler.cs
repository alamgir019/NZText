using NZ.HRM.Application.EmployeeNatures.Commands.CreateEmployeeNature;
using NZ.HRM.Application.EmployeeNatures.Commands.DeleteEmployeeNature;
using NZ.HRM.Application.EmployeeNatures.Commands.UpdateEmployeeNature;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeeNatures.Handlers;

public class EmployeeNatureCommandHandler
{
    private readonly IEmployeeNatureRepository _employeeNatureRepository;

    public EmployeeNatureCommandHandler(IEmployeeNatureRepository employeeNatureRepository)
    {
        _employeeNatureRepository = employeeNatureRepository;
    }

    public async Task<string> Handle(CreateEmployeeNatureCommand command, CancellationToken cancellationToken = default)
    {
        var employeeNature = new LookEmployeeNature
        {
            NatureName = command.NatureName,
            SortOrder = command.SortOrder,
            IsActive = true
        };

        return await _employeeNatureRepository.AddAsync(employeeNature, cancellationToken);
    }

    public async Task Handle(UpdateEmployeeNatureCommand command, CancellationToken cancellationToken = default)
    {
        var employeeNature = await _employeeNatureRepository.GetByIdAsync(command.Id, cancellationToken);
        if (employeeNature == null)
            throw new KeyNotFoundException($"Employee nature with ID {command.Id} not found");

        employeeNature.NatureName = command.NatureName;
        employeeNature.SortOrder = command.SortOrder;

        await _employeeNatureRepository.UpdateAsync(employeeNature, cancellationToken);
    }

    public async Task Handle(DeleteEmployeeNatureCommand command, CancellationToken cancellationToken = default)
    {
        var employeeNature = await _employeeNatureRepository.GetByIdAsync(command.Id, cancellationToken);
        if (employeeNature == null)
            throw new KeyNotFoundException($"Employee nature with ID {command.Id} not found");

        employeeNature.IsActive = false;
        await _employeeNatureRepository.UpdateAsync(employeeNature, cancellationToken);
    }
}
