using NZ.HRM.Application.Departments.Commands.CreateDepartment;
using NZ.HRM.Application.Departments.Commands.DeleteDepartment;
using NZ.HRM.Application.Departments.Commands.UpdateDepartment;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Departments.Handlers;

public class DepartmentCommandHandler
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<string> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var department = new Department
        {
            DepartmentName = command.DepartmentName,
            DepartmentCode = command.DepartmentCode,
            IsActive = true
        };

        return await _departmentRepository.AddAsync(department, cancellationToken);
    }

    public async Task Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(command.Id, cancellationToken);

        if (department == null)
            throw new KeyNotFoundException($"Department with ID {command.Id} not found");

        department.DepartmentName = command.DepartmentName;
        department.DepartmentCode = command.DepartmentCode;

        await _departmentRepository.UpdateAsync(department, cancellationToken);
    }

    public async Task Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(command.Id, cancellationToken);

        if (department == null)
            throw new KeyNotFoundException($"Department with ID {command.Id} not found");

        // Soft delete
        department.IsActive = false;
        await _departmentRepository.UpdateAsync(department, cancellationToken);

        // Or hard delete
        // await _departmentRepository.DeleteAsync(department, cancellationToken);
    }
}
