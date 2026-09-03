namespace NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

public class EligibleLearnerSummaryDto
{
    public int TotalEligibleLearners { get; set; }
    public int ReadyForAdjustment { get; set; }
    public decimal TotalAdjustmentAmount { get; set; }
    public decimal AverageAdjustmentAmount { get; set; }
}

public class EligibleLearnerPaginationDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
}

public class EligibleLearnerSearchResultDto
{
    public EligibleLearnerSummaryDto Summary { get; set; } = new();
    public EligibleLearnerPaginationDto Pagination { get; set; } = new();
    public List<EligibleLearnerDto> Learners { get; set; } = new();
}
