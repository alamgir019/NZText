using NZ.HRM.Application.Common;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.LearnerAdjustments.Queries;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;
using NZ.HRM.Domain.Services;

namespace NZ.HRM.Application.LearnerAdjustments.Handlers;

public class EligibleLearnerQueryHandler
{
    private readonly IEligibleLearnerRepository _repository;
    private readonly IBusinessCalendar _businessCalendar;

    public EligibleLearnerQueryHandler(
        IEligibleLearnerRepository repository,
        IBusinessCalendar businessCalendar)
    {
        _repository = repository;
        _businessCalendar = businessCalendar;
    }

    public Task<EligibleLearnerSearchResultDto> Handle(
        SearchEligibleLearnersQuery query,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(query, noPaging: false, cancellationToken);

    /// <summary>
    /// Returns the complete filtered result set (used by the export endpoint).
    /// </summary>
    public Task<EligibleLearnerSearchResultDto> HandleForExport(
        SearchEligibleLearnersQuery query,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(query, noPaging: true, cancellationToken);

    private async Task<EligibleLearnerSearchResultDto> ExecuteAsync(
        SearchEligibleLearnersQuery query, bool noPaging, CancellationToken cancellationToken)
    {
        var filter = BuildFilter(query, noPaging);

        var (learners, totalRecords, totalAdjustment) =
            await _repository.GetEligibleLearnersAsync(filter, cancellationToken);

        var average = totalRecords == 0
            ? 0m
            : decimal.Round(totalAdjustment / totalRecords, 2, MidpointRounding.AwayFromZero);

        return new EligibleLearnerSearchResultDto
        {
            Summary = new EligibleLearnerSummaryDto
            {
                TotalEligibleLearners = totalRecords,
                ReadyForAdjustment = totalRecords,
                TotalAdjustmentAmount = decimal.Round(totalAdjustment, 2, MidpointRounding.AwayFromZero),
                AverageAdjustmentAmount = average
            },
            Pagination = new EligibleLearnerPaginationDto
            {
                PageNumber = filter.NoPaging ? 1 : filter.PageNumber,
                PageSize = filter.NoPaging ? totalRecords : filter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = filter.NoPaging
                    ? (totalRecords == 0 ? 0 : 1)
                    : (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            },
            Learners = learners
        };
    }

    private EligibleLearnerFilter BuildFilter(SearchEligibleLearnersQuery query, bool noPaging)
    {
        if (query.JoiningDateFrom is null || query.JoiningDateTo is null)
            throw new BusinessRuleException("INVALID_DATE_RANGE",
                "Joining From Date and Joining To Date are required.");

        if (query.JoiningDateFrom > query.JoiningDateTo)
            throw new BusinessRuleException("INVALID_DATE_RANGE",
                "Joining From Date cannot be greater than Joining To Date.");

        if (query.ProbationPeriodMonths <= 0 ||
            !ProbationAdjustmentPolicy.IsSupportedProbationPeriod(query.ProbationPeriodMonths))
            throw new BusinessRuleException("INVALID_PROBATION_PERIOD",
                "Selected probation period is not valid.");

        var pageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;
        var pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

        return new EligibleLearnerFilter(
            query.JoiningDateFrom.Value,
            query.JoiningDateTo.Value,
            query.ProbationPeriodMonths,
            _businessCalendar.CurrentBusinessDate,
            pageNumber,
            pageSize,
            noPaging);
    }
}
