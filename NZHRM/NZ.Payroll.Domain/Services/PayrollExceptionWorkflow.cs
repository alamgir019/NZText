using NZ.HRM.Domain.Entities;
using NZ.Payroll.Domain.Enums;

namespace NZ.Payroll.Domain.Services;

public class PayrollExceptionWorkflow
{
    public void ForwardToIT(PayPayrollException entity, string processedBy)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (string.IsNullOrWhiteSpace(processedBy))
        {
            throw new ArgumentException("Processed by is required.", nameof(processedBy));
        }

        if (!string.Equals(entity.Status, PayrollExceptionStatuses.Pending, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Only pending payroll exception requests can be forwarded to Head Office IT.");
        }

        entity.Status = PayrollExceptionStatuses.ForwardedToIT;
        entity.ResolvedBy = processedBy;
        entity.ResolvedDate = DateTime.UtcNow;
        entity.UpdatedBy = processedBy;
        entity.UpdatedOn = DateTime.UtcNow;
    }
}
