/**
 * SALARY BREAKDOWN SERVICE - IMPLEMENTATION SUMMARY
 * 
 * FILE CREATED: NZHRM/NZ.HRM.Application/Payroll/Services/SalaryBreakdownService.cs
 * 
 * PURPOSE:
 * Calculates salary component breakup (Conveyance, Basic, House Rent, Medical, Food) 
 * from Gross Salary based on Employee Nature (Worker, Staff, Management).
 * 
 * ============================================================================
 * FORMULAS IMPLEMENTED
 * ============================================================================
 * 
 * FOR STAFF/MANAGEMENT:
 * --------------------
 * Initial Calculation:
 *   - Conveyance = 2,500
 *   - Basic = (Gross - Conveyance) / 1.6
 *   - House Rent = Basic × 50%
 *   - Medical = Basic × 10%
 * 
 * If House Rent > 25,000:
 *   - Conveyance = 2,500
 *   - Basic = (Gross - Conveyance) / 1.1  (RECALCULATED)
 *   - House Rent = 25,000 (CAPPED)
 *   - Medical = Basic × 10%
 * 
 * 
 * FOR WORKER:
 * -----------
 * - Medical = 750
 * - Conveyance = 400
 * - Food = 850
 * - Basic = (Gross - (Medical + Conveyance + Food)) / 1.55
 * - House Rent = Basic × 55%
 * 
 * ============================================================================
 * USAGE IN CODE
 * ============================================================================
 * 
 * // Static method call in EmployeeCommandHandler:
 * var salaryBreakdown = SalaryBreakdownService.CalculateSalaryBreakdown(
 *     grossSalary: 50000m,
 *     employeeNature: EmployeeNature.Staff
 * );
 * 
 * // Returns SalaryBreakdownDto with properties:
 * - decimal Conveyance
 * - decimal Basic
 * - decimal HouseRent
 * - decimal Medical
 * - decimal? Food (only for Worker)
 * 
 * ============================================================================
 * INTEGRATION POINTS
 * ============================================================================
 * 
 * 1. EmployeeCommandHandler.cs
 *    - Private method: CalculateSalaryBreakdown()
 *    - Used in: UpsertPayroll() at line ~458
 *    - Calculates components when creating/updating employee payroll
 *    - Uses calculated Basic as default for BankPortion if not provided
 * 
 * 2. Currently used to auto-fill:
 *    - payroll.BankPortion = salaryBreakdown.Basic (if not explicitly provided)
 *    - payroll.CashPortion = 0 (if not explicitly provided)
 * 
 * ============================================================================
 * EXAMPLE CALCULATIONS
 * ============================================================================
 * 
 * STAFF EXAMPLE:
 * Gross = 50,000
 * Conveyance = 2,500
 * Basic = (50,000 - 2,500) / 1.6 = 29,687.50
 * House Rent = 29,687.50 × 0.5 = 14,843.75 (≤ 25,000, so valid)
 * Medical = 29,687.50 × 0.1 = 2,968.75
 * 
 * 
 * WORKER EXAMPLE:
 * Gross = 10,000
 * Medical = 750
 * Conveyance = 400
 * Food = 850
 * Basic = (10,000 - (750 + 400 + 850)) / 1.55 = 5,548.39
 * House Rent = 5,548.39 × 0.55 = 3,051.61
 * 
 * ============================================================================
 * FUTURE ENHANCEMENTS
 * ============================================================================
 * 
 * 1. Store calculated components in database:
 *    - Add fields to HrmEmployeePayroll for Conveyance, HouseRent, etc.
 *    - Persist the breakdown along with the payroll record
 * 
 * 2. Create payslip calculation service:
 *    - Use SalaryBreakdownDto components for detailed payslips
 *    - Calculate deductions and net salary
 * 
 * 3. Validation service:
 *    - Validate if provided BankPortion/CashPortion match calculated values
 *    - Issue warnings/errors if discrepancies found
 * 
 * 4. Update service:
 *    - Expose method to recalculate if employee nature or gross salary changes
 * 
 * ============================================================================
 */
