using NZ.HRM.Application.EmployeeMasters.Queries.VerifyEmployeeCodeUniqueness;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.EmployeeMasters.Handlers;

public class EmployeeMasterQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public EmployeeMasterQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeCodeUniquenessDto> Handle(VerifyEmployeeCodeUniquenessQuery query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query.EmployeeCode))
        {
            return new EmployeeCodeUniquenessDto
            {
                IsUnique = false,
                Message = "Employee code cannot be empty"
            };
        }

        // Check uniqueness at database level - efficient single query
        var isUnique = await _employeeMasterRepository.IsEmployeeCodeUniqueAsync(
            query.EmployeeCode,
            cancellationToken);

        return new EmployeeCodeUniquenessDto
        {
            IsUnique = isUnique,
            Message = isUnique 
                ? "Employee old code is available" 
                : $"Employee old code '{query.EmployeeCode}' is already in use"
        };
    }
}
