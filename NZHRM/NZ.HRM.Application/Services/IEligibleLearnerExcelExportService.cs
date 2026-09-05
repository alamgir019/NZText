using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;

namespace NZ.HRM.Application.Services;

public interface IEligibleLearnerExcelExportService
{
    Task<byte[]> GenerateEligibleLearnersExcelAsync(List<EligibleLearnerDto> learners);
}
