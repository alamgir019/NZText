using System.Linq;

namespace NZ.HRM.Domain.Services
{
    /// <summary>
    /// Domain rules for learner probation completion and salary adjustment eligibility.
    /// </summary>
    public static class ProbationAdjustmentPolicy
    {
        public const string LearnerDesignationName = "Learner";
        public const string StandardWorkerDesignationName = "Worker";
        public const string ActiveStatus = "Active";

        /// <summary>
        /// Probation periods (in months) supported by the system.
        /// </summary>
        public static readonly int[] SupportedProbationPeriods = { 1, 2, 3, 6, 12 };

        public static bool IsSupportedProbationPeriod(int months)
            => SupportedProbationPeriods.Contains(months);

        public static DateOnly CalculateProbationCompletedOn(DateOnly dateOfJoining, int probationPeriodMonths)
            => dateOfJoining.AddMonths(probationPeriodMonths);

        public static bool IsProbationCompleted(DateOnly dateOfJoining, int probationPeriodMonths, DateOnly businessDate)
            => CalculateProbationCompletedOn(dateOfJoining, probationPeriodMonths) <= businessDate;

        /// <summary>
        /// Adjustment = Standard Gross Salary - Current Gross Salary. Negative values are returned as 0.00.
        /// </summary>
        public static decimal CalculateAdjustmentAmount(decimal standardGrossSalary, decimal currentGrossSalary)
        {
            var amount = standardGrossSalary - currentGrossSalary;
            return amount < 0m ? 0m : decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
