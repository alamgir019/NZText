using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Payroll.Services;

public class SalaryBreakdownDto
{
    public decimal Conveyance { get; set; }
    public decimal Basic { get; set; }
    public decimal HouseRent { get; set; }
    public decimal Medical { get; set; }
    public decimal? Food { get; set; } // Only for Worker
}

public class SalaryBreakdownService
{
    private const decimal STAFF_CONVEYANCE = 2500m;
    private const decimal STAFF_HOUSE_RENT_MAX = 25000m;
    private const decimal WORKER_MEDICAL = 750m;
    private const decimal WORKER_CONVEYANCE = 400m;
    private const decimal WORKER_FOOD = 850m;
    private const decimal STAFF_HOUSE_RENT_PERCENTAGE = 0.5m;
    private const decimal STAFF_MEDICAL_PERCENTAGE = 0.1m;
    private const decimal WORKER_HOUSE_RENT_PERCENTAGE = 0.55m;

    /// <summary>
    /// Calculates salary breakup from gross salary based on employee nature.
    /// </summary>
    /// <param name="grossSalary">Gross salary amount</param>
    /// <param name="employeeNature">Type of employee (Worker, Staff, or Management)</param>
    /// <returns>SalaryBreakdownDto containing calculated components</returns>
    public static SalaryBreakdownDto CalculateSalaryBreakdown(decimal grossSalary, EmployeeNature employeeNature)
    {
        return employeeNature switch
        {
            EmployeeNature.Worker => CalculateWorkerSalary(grossSalary),
            EmployeeNature.Staff => CalculateStaffSalary(grossSalary),
            EmployeeNature.Management => CalculateStaffSalary(grossSalary), // Management uses same formula as Staff
            _ => throw new ArgumentException($"Unknown employee nature: {employeeNature}")
        };
    }

    /// <summary>
    /// Calculates salary breakup for Staff/Management employees.
    /// 
    /// Formula:
    /// Conveyance = 2500
    /// If House Rent (Basic × 50%) ≤ 25,000:
    ///     Basic = (Gross - Conveyance) / 1.6
    ///     House Rent = Basic × 50%
    /// Otherwise:
    ///     Basic = (Gross - Conveyance) / 1.1
    ///     House Rent = 25,000
    /// Medical = Basic × 10%
    /// </summary>
    private static SalaryBreakdownDto CalculateStaffSalary(decimal grossSalary)
    {
        decimal conveyance = STAFF_CONVEYANCE;

        // Calculate tentative Basic using standard divisor
        decimal tentativeBasic = (grossSalary - conveyance) / 1.6m;
        decimal tentativeHouseRent = tentativeBasic * STAFF_HOUSE_RENT_PERCENTAGE;

        // If house rent exceeds max, recalculate with different divisor
        decimal basic;
        decimal houseRent;

        if (tentativeHouseRent > STAFF_HOUSE_RENT_MAX)
        {
            basic = (grossSalary - conveyance) / 1.1m;
            houseRent = STAFF_HOUSE_RENT_MAX;
        }
        else
        {
            basic = tentativeBasic;
            houseRent = tentativeHouseRent;
        }

        decimal medical = basic * STAFF_MEDICAL_PERCENTAGE;

        return new SalaryBreakdownDto
        {
            Conveyance = conveyance,
            Basic = basic,
            HouseRent = houseRent,
            Medical = medical
        };
    }

    /// <summary>
    /// Calculates salary breakup for Worker employees.
    /// 
    /// Formula:
    /// Medical = 750
    /// Conveyance = 400
    /// Food = 850
    /// Basic = (Gross - (Medical + Conveyance + Food)) / 1.55
    /// House Rent = Basic × 55%
    /// </summary>
    private static SalaryBreakdownDto CalculateWorkerSalary(decimal grossSalary)
    {
        decimal medical = WORKER_MEDICAL;
        decimal conveyance = WORKER_CONVEYANCE;
        decimal food = WORKER_FOOD;

        decimal totalFixedAllowances = medical + conveyance + food;
        decimal basic = (grossSalary - totalFixedAllowances) / 1.55m;
        decimal houseRent = basic * WORKER_HOUSE_RENT_PERCENTAGE;

        return new SalaryBreakdownDto
        {
            Conveyance = conveyance,
            Basic = basic,
            HouseRent = houseRent,
            Medical = medical,
            Food = food
        };
    }
}
