namespace NZ.Shared.Events.IntegrationEvents;

/// <summary>Raised when a new employee is created in the Employee module.</summary>
public class EmployeeCreatedEvent : IntegrationEvent
{
    public EmployeeCreatedEvent(string employeeId, string employeeCode, string employeeName)
        : base("Employee")
    {
        EmployeeId = employeeId;
        EmployeeCode = employeeCode;
        EmployeeName = employeeName;
    }

    public string EmployeeId { get; }
    public string EmployeeCode { get; }
    public string EmployeeName { get; }
    public override string EventType => "EmployeeCreated";
}

/// <summary>Raised when payroll is processed for a period.</summary>
public class PayrollProcessedEvent : IntegrationEvent
{
    public PayrollProcessedEvent(string payrollGroupId, int year, int month, int employeeCount)
        : base("Payroll")
    {
        PayrollGroupId = payrollGroupId;
        Year = year;
        Month = month;
        EmployeeCount = employeeCount;
    }

    public string PayrollGroupId { get; }
    public int Year { get; }
    public int Month { get; }
    public int EmployeeCount { get; }
    public override string EventType => "PayrollProcessed";
}

/// <summary>Raised when attendance data is synced from a device.</summary>
public class AttendanceSyncedEvent : IntegrationEvent
{
    public AttendanceSyncedEvent(string deviceId, string unit, int recordCount, DateOnly syncDate)
        : base("Attendance")
    {
        DeviceId = deviceId;
        Unit = unit;
        RecordCount = recordCount;
        SyncDate = syncDate;
    }

    public string DeviceId { get; }
    public string Unit { get; }
    public int RecordCount { get; }
    public DateOnly SyncDate { get; }
    public override string EventType => "AttendanceSynced";
}

/// <summary>Raised when a leave application is approved.</summary>
public class LeaveApprovedEvent : IntegrationEvent
{
    public LeaveApprovedEvent(string leaveApplicationId, string employeeId, string leaveTypeCode, DateOnly from, DateOnly to)
        : base("Leave")
    {
        LeaveApplicationId = leaveApplicationId;
        EmployeeId = employeeId;
        LeaveTypeCode = leaveTypeCode;
        From = from;
        To = to;
    }

    public string LeaveApplicationId { get; }
    public string EmployeeId { get; }
    public string LeaveTypeCode { get; }
    public DateOnly From { get; }
    public DateOnly To { get; }
    public override string EventType => "LeaveApproved";
}

/// <summary>Raised when an employee's loan is disbursed.</summary>
public class LoanDisbursedEvent : IntegrationEvent
{
    public LoanDisbursedEvent(string loanId, string employeeId, decimal amount)
        : base("Loan")
    {
        LoanId = loanId;
        EmployeeId = employeeId;
        Amount = amount;
    }

    public string LoanId { get; }
    public string EmployeeId { get; }
    public decimal Amount { get; }
    public override string EventType => "LoanDisbursed";
}

/// <summary>Raised when a performance review is completed.</summary>
public class PerformanceReviewCompletedEvent : IntegrationEvent
{
    public PerformanceReviewCompletedEvent(string reviewId, string employeeId, decimal score)
        : base("Performance")
    {
        ReviewId = reviewId;
        EmployeeId = employeeId;
        Score = score;
    }

    public string ReviewId { get; }
    public string EmployeeId { get; }
    public decimal Score { get; }
    public override string EventType => "PerformanceReviewCompleted";
}
