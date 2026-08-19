namespace NZ.Payroll.Domain.Enums;

public enum PayrollStatus
{
    Draft,
    Processing,
    Completed,
    Locked,
    Cancelled
}

public enum PayrollFrequency
{
    Monthly,
    BiWeekly,
    Weekly
}
