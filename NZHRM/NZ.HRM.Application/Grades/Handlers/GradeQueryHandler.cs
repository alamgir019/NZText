using NZ.HRM.Application.Grades.Queries.GetAllGrades;
using NZ.HRM.Application.Grades.Queries.GetGradeById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Grades.Handlers;

public class GradeQueryHandler
{
    private readonly IGradeRepository _gradeRepository;

    public GradeQueryHandler(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<List<GradeDto>> Handle(GetAllGradesQuery query, CancellationToken cancellationToken = default)
    {
        var grades = await _gradeRepository.GetAllAsync(query.IncludeInactive, query.EmployeeType, cancellationToken);

        return grades.Select(g => new GradeDto
        {
            Id = g.Id,
            GradeName = g.GradeName,
            //MinSalary = g.MinSalary,
            //MaxSalary = g.MaxSalary,
            //EmployeeType = g.EmployeeType,
            CreatedOn = g.CreatedOn,
            CreatedBy = g.CreatedBy,
            UpdatedOn = g.UpdatedOn,
            UpdatedBy = g.UpdatedBy,
            IsActive = g.IsActive
        }).ToList();
    }

    public async Task<GradeDetailDto?> Handle(GetGradeByIdQuery query, CancellationToken cancellationToken = default)
    {
        var grade = await _gradeRepository.GetByIdAsync(query.Id, cancellationToken);

        if (grade == null)
            return null;

        return new GradeDetailDto
        {
            Id = grade.Id,
            GradeName = grade.GradeName,
            //MinSalary = grade.MinSalary,
            //MaxSalary = grade.MaxSalary,
            //EmployeeType = grade.EmployeeType,
            CreatedOn = grade.CreatedOn,
            CreatedBy = grade.CreatedBy,
            UpdatedOn = grade.UpdatedOn,
            UpdatedBy = grade.UpdatedBy,
            IsActive = grade.IsActive
        };
    }
}
