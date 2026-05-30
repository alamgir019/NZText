using NZ.HRM.Application.Grades.Commands.CreateGrade;
using NZ.HRM.Application.Grades.Commands.DeleteGrade;
using NZ.HRM.Application.Grades.Commands.UpdateGrade;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Grades.Handlers;

public class GradeCommandHandler
{
    private readonly IGradeRepository _gradeRepository;

    public GradeCommandHandler(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<string> Handle(CreateGradeCommand command, CancellationToken cancellationToken = default)
    {
        // Validate that MaxSalary is greater than MinSalary
        if (command.MaxSalary <= command.MinSalary)
        {
            throw new ArgumentException("Maximum salary must be greater than minimum salary");
        }

        var grade = new Grade
        {
            GradeName = command.GradeName,
            MinSalary = command.MinSalary,
            MaxSalary = command.MaxSalary,
            EmployeeType = command.EmployeeType,
            IsActive = true
        };

        return await _gradeRepository.AddAsync(grade, cancellationToken);
    }

    public async Task Handle(UpdateGradeCommand command, CancellationToken cancellationToken = default)
    {
        var grade = await _gradeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (grade == null)
            throw new KeyNotFoundException($"Grade with ID {command.Id} not found");

        // Validate that MaxSalary is greater than MinSalary
        if (command.MaxSalary <= command.MinSalary)
        {
            throw new ArgumentException("Maximum salary must be greater than minimum salary");
        }

        grade.GradeName = command.GradeName;
        grade.MinSalary = command.MinSalary;
        grade.MaxSalary = command.MaxSalary;
        grade.EmployeeType = command.EmployeeType;

        await _gradeRepository.UpdateAsync(grade, cancellationToken);
    }

    public async Task Handle(DeleteGradeCommand command, CancellationToken cancellationToken = default)
    {
        var grade = await _gradeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (grade == null)
            throw new KeyNotFoundException($"Grade with ID {command.Id} not found");

        // Soft delete
        grade.IsActive = false;
        await _gradeRepository.UpdateAsync(grade, cancellationToken);

        // Or hard delete
        // await _gradeRepository.DeleteAsync(grade, cancellationToken);
    }
}
