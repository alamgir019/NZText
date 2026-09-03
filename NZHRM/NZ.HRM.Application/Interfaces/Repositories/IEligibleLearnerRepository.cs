using NZ.HRM.Application.LearnerAdjustments.Queries;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IEligibleLearnerRepository
{
    /// <summary>
    /// Returns the requested page of eligible learners along with the total record count and the
    /// total adjustment amount computed across the entire filtered result set.
    /// </summary>
    Task<(List<EligibleLearnerDto> Learners, int TotalRecords, decimal TotalAdjustmentAmount)>
        GetEligibleLearnersAsync(EligibleLearnerFilter filter, CancellationToken cancellationToken = default);
}
