using NZ.HRM.Application.EmployeeMasters.Queries.GetEnrollmentId;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.EmployeeMasters.Handlers;

public class GetEnrollmentIdQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public GetEnrollmentIdQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<string> Handle(GetEnrollmentIdQuery query, CancellationToken cancellationToken = default)
    {
        // Use the provided date (caller should pass UTC DateTime)
        var today = query.Today.Date;
        var next = await _employeeMasterRepository.GetNextEnrollmentIdAsync(today, cancellationToken: cancellationToken);
        return next;
    }
}
