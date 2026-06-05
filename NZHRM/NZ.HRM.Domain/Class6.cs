using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     DOCUMENT 43D
SYNEXIS ERP DATABASE DICTIONARY
PART 4 – PAYROLL DATABASE TABLES
________________________________________
PAYROLL MODULE PHILOSOPHY
VERY IMPORTANT
The Payroll Module is the financial heart of SYNEXIS ERP.
Payroll must NEVER depend on manual entry.
Payroll shall be generated from:
Employee Master
        +
Attendance
        +
Leave
        +
OT
        +
Increment
        +
Loan Recovery
        +
Tax
        ↓
Payroll Engine
        ↓
Payslip
________________________________________
PAYROLL ARCHITECTURE
PAY_SalaryStructure
          │
          ▼
PAY_PayrollHeader
          │
          ▼
PAY_PayrollDetails
          │
          ▼
PAY_Payslip
________________________________________
TABLE 1
PAY_SalaryStructure
Purpose:
Stores employee salary structure.
________________________________________
Fields
Field	Type
SalaryStructureID	BIGINT PK
EmployeeID	BIGINT FK
EffectiveDate	DATE
BasicSalary	DECIMAL(18,2)
HouseRent	DECIMAL(18,2)
MedicalAllowance	DECIMAL(18,2)
ConveyanceAllowance	DECIMAL(18,2)
FoodAllowance	DECIMAL(18,2)
OtherAllowance	DECIMAL(18,2)
GrossSalary	DECIMAL(18,2)
ActiveFlag	BIT
CreatedDate	DATETIME
________________________________________
TABLE 2
PAY_IncrementHistory
Purpose:
Tracks all salary revisions.
________________________________________
Fields
Field	Type
IncrementID	BIGINT PK
EmployeeID	BIGINT FK
EffectiveDate	DATE
OldGrossSalary	DECIMAL(18,2)
NewGrossSalary	DECIMAL(18,2)
IncrementAmount	DECIMAL(18,2)
IncrementPercent	DECIMAL(8,2)
ApprovedBy	BIGINT FK
ApprovalDate	DATETIME
________________________________________
TABLE 3
PAY_PayrollHeader
Purpose:
Payroll month control.
________________________________________
Fields
Field	Type
PayrollID	BIGINT PK
PayrollMonth	VARCHAR(7)
GroupID	BIGINT FK
UnitID	BIGINT FK
PayrollStatus	VARCHAR(20)
TotalEmployees	INT
TotalGross	DECIMAL(18,2)
TotalDeduction	DECIMAL(18,2)
TotalNetSalary	DECIMAL(18,2)
ProcessedDate	DATETIME
ApprovedDate	DATETIME
________________________________________
Status
•	Draft 
•	Processed 
•	Approved 
•	Locked 
________________________________________
TABLE 4
PAY_PayrollDetails
MOST IMPORTANT PAYROLL TABLE
One employee = one record per payroll month.
________________________________________
Fields
Field	Type
PayrollDetailID	BIGINT PK
PayrollID	BIGINT FK
EmployeeID	BIGINT FK
GrossSalary	DECIMAL(18,2)
PayableDays	DECIMAL(8,2)
WorkedDays	DECIMAL(8,2)
OTAmount	DECIMAL(18,2)
BonusAmount	DECIMAL(18,2)
ArrearAmount	DECIMAL(18,2)
DeductionAmount	DECIMAL(18,2)
TaxAmount	DECIMAL(18,2)
LoanRecovery	DECIMAL(18,2)
NetSalary	DECIMAL(18,2)
________________________________________
TABLE 5
PAY_OTDetails
Purpose:
Stores OT calculations.
________________________________________
Fields
Field	Type
OTDetailID	BIGINT PK
PayrollDetailID	BIGINT FK
EmployeeID	BIGINT FK
PayrollMonth	VARCHAR(7)
TotalOTHours	DECIMAL(8,2)
OTRate	DECIMAL(18,4)
OTAmount	DECIMAL(18,2)
________________________________________
TABLE 6
PAY_Deduction
Purpose:
Stores deduction details.
________________________________________
Fields
Field	Type
DeductionID	BIGINT PK
PayrollDetailID	BIGINT FK
EmployeeID	BIGINT FK
DeductionType	VARCHAR(50)
DeductionAmount	DECIMAL(18,2)
Remarks	VARCHAR(500)
________________________________________
Examples
•	LWP 
•	Loan 
•	Salary Advance 
•	Tax 
•	Welfare Fund 
•	Penalty 
________________________________________
TABLE 7
PAY_Arrear
Purpose:
Stores arrear calculations.
________________________________________
Fields
Field	Type
ArrearID	BIGINT PK
EmployeeID	BIGINT FK
PayrollMonth	VARCHAR(7)
ArrearType	VARCHAR(50)
ArrearAmount	DECIMAL(18,2)
EffectiveFrom	DATE
EffectiveTo	DATE
Status	VARCHAR(20)
________________________________________
TABLE 8
PAY_Bonus
Purpose:
Festival and special bonuses.
________________________________________
Fields
Field	Type
BonusID	BIGINT PK
EmployeeID	BIGINT FK
BonusType	VARCHAR(50)
BonusAmount	DECIMAL(18,2)
BonusDate	DATE
PayrollMonth	VARCHAR(7)
ApprovedBy	BIGINT FK
________________________________________
Examples
•	Eid Bonus 
•	Performance Bonus 
•	Production Bonus 
•	Attendance Bonus 
________________________________________
TABLE 9
PAY_Tax
Purpose:
Income tax calculation.
________________________________________
Fields
Field	Type
TaxID	BIGINT PK
EmployeeID	BIGINT FK
PayrollMonth	VARCHAR(7)
TaxableIncome	DECIMAL(18,2)
TaxAmount	DECIMAL(18,2)
TaxRuleID	BIGINT
CalculationDate	DATETIME
________________________________________
TABLE 10
PAY_LoanRecovery
Purpose:
Links payroll and loan module.
________________________________________
Fields
Field	Type
RecoveryID	BIGINT PK
PayrollDetailID	BIGINT FK
LoanID	BIGINT FK
EmployeeID	BIGINT FK
RecoveryAmount	DECIMAL(18,2)
BalanceAfterRecovery	DECIMAL(18,2)
________________________________________
TABLE 11
PAY_BankTransfer
Purpose:
Salary bank transfer preparation.
________________________________________
Fields
Field	Type
TransferID	BIGINT PK
PayrollID	BIGINT FK
EmployeeID	BIGINT FK
BankName	VARCHAR(100)
AccountNo	VARCHAR(50)
TransferAmount	DECIMAL(18,2)
TransferStatus	VARCHAR(20)
TransferDate	DATETIME
________________________________________
TABLE 12
PAY_Payslip
Purpose:
Stores generated payslips.
________________________________________
Fields
Field	Type
PayslipID	BIGINT PK
PayrollDetailID	BIGINT FK
EmployeeID	BIGINT FK
PayrollMonth	VARCHAR(7)
PayslipFilePath	VARCHAR(1000)
GeneratedDate	DATETIME
GeneratedBy	BIGINT FK
________________________________________
TABLE 13
PAY_PayrollAdjustment
Purpose:
Exceptional payroll correction.
________________________________________
Fields
Field	Type
AdjustmentID	BIGINT PK
EmployeeID	BIGINT FK
PayrollMonth	VARCHAR(7)
AdjustmentType	VARCHAR(50)
OldAmount	DECIMAL(18,2)
NewAmount	DECIMAL(18,2)
Reason	VARCHAR(500)
ApprovedBy	BIGINT FK
AdjustmentDate	DATETIME
________________________________________
TABLE 14
PAY_PayrollLock
VERY IMPORTANT
Purpose:
Locks payroll after approval.
________________________________________
Fields
Field	Type
LockID	BIGINT PK
PayrollMonth	VARCHAR(7)
UnitID	BIGINT FK
LockedBy	BIGINT FK
LockDate	DATETIME
UnlockBy	BIGINT FK
UnlockDate	DATETIME
Status	VARCHAR(20)
________________________________________
TABLE 15
PAY_PayrollProcessLog
Purpose:
Tracks payroll engine execution.
________________________________________
Fields
Field	Type
ProcessLogID	BIGINT PK
PayrollMonth	VARCHAR(7)
StartTime	DATETIME
EndTime	DATETIME
EmployeeCount	INT
ProcessedCount	INT
ExceptionCount	INT
ProcessedBy	BIGINT FK
________________________________________
PAYROLL RELATIONSHIP FLOW
HRM_EmployeeMaster
        │
        ▼
PAY_SalaryStructure
        │
        ▼
ATT_ProcessedAttendance
        │
        ▼
PAY_PayrollHeader
        │
        ▼
PAY_PayrollDetails
        │
 ┌──────┼───────┐
 ▼      ▼       ▼
OT    Tax    Loan
 ▼      ▼       ▼
PAY_OTDetails
PAY_Tax
PAY_LoanRecovery
        │
        ▼
PAY_Payslip
________________________________________
PAYROLL TABLE SUMMARY
Table	Purpose
PAY_SalaryStructure	Salary Structure
PAY_IncrementHistory	Salary Revision
PAY_PayrollHeader	Payroll Month
PAY_PayrollDetails	Employee Payroll
PAY_OTDetails	OT
PAY_Deduction	Deductions
PAY_Arrear	Arrears
PAY_Bonus	Bonuses
PAY_Tax	Income Tax
PAY_LoanRecovery	Loan Recovery
PAY_BankTransfer	Salary Transfer
PAY_Payslip	Payslip
PAY_PayrollAdjustment	Payroll Correction
PAY_PayrollLock	Payroll Lock
PAY_PayrollProcessLog	Process Log
Total Payroll Tables = 15
________________________________________
IMPORTANT OBSERVATION
At this point you now have:
Part 1
Organization & HR Tables
Part 2
Attendance Tables
Part 3
Leave Tables
Part 4
Payroll Tables
These four sections together represent roughly:
70–75% of the entire SYNEXIS ERP database.


     */
    internal class Class6
    {
    }
}
