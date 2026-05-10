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
        var all = await _employeeMasterRepository.GetAllAsync(today, includeInactive: true, cancellationToken: cancellationToken);
        var todaysCount = all.Count; // repository already filtered by date when provided
        var sequence = todaysCount + 1;
        var datePart = today.ToString("ddMMyy");
        var enrollmentId = $"{datePart}{sequence:D3}";
        return enrollmentId;
    }
}
