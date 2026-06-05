using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     PART 3 – LEAVE MANAGEMENT DATABASE TABLES
________________________________________
LEAVE MODULE PHILOSOPHY
VERY IMPORTANT
Leave is not merely an application process.
In SYNEXIS:
Leave Balance
      ↓
Leave Application
      ↓
Workflow Approval
      ↓
Attendance Integration
      ↓
Payroll Impact
Therefore Leave must integrate directly with:
•	Employee Master 
•	Workflow Engine 
•	Attendance Engine 
•	Payroll Engine 
________________________________________
TABLE 1
MST_LeaveType
Purpose:
Defines all leave categories.
________________________________________
Fields
Field	Type
LeaveTypeID	BIGINT PK
LeaveCode	VARCHAR(20)
LeaveName	VARCHAR(100)
LeaveCategory	VARCHAR(50)
AnnualEntitlement	DECIMAL(5,2)
Encashable	BIT
CarryForwardAllowed	BIT
MaxCarryForwardDays	DECIMAL(5,2)
ApprovalRequired	BIT
Status	BIT
________________________________________
Examples
Leave
Casual Leave
Sick Leave
Earned Leave
Maternity Leave
Special Leave
Leave Without Pay
________________________________________
TABLE 2
LEV_LeaveBalance
MOST IMPORTANT TABLE
Stores current leave balance.
________________________________________
Fields
Field	Type
BalanceID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
YearID	INT
OpeningBalance	DECIMAL(6,2)
EarnedLeave	DECIMAL(6,2)
AvailedLeave	DECIMAL(6,2)
AdjustedLeave	DECIMAL(6,2)
EncashedLeave	DECIMAL(6,2)
ClosingBalance	DECIMAL(6,2)
LastUpdated	DATETIME
________________________________________
TABLE 3
LEV_LeaveApplication
MOST IMPORTANT TABLE
Stores leave requests.
________________________________________
Fields
Field	Type
LeaveApplicationID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
FromDate	DATE
ToDate	DATE
TotalDays	DECIMAL(5,2)
LeaveReason	VARCHAR(500)
ApplicationDate	DATETIME
WorkflowID	BIGINT FK
LeaveStatus	VARCHAR(20)
ApprovedBy	BIGINT FK
ApprovalDate	DATETIME
________________________________________
Status Values
•	Draft 
•	Pending 
•	Approved 
•	Rejected 
•	Cancelled 
________________________________________
TABLE 4
LEV_LeaveApplicationDetails
Purpose:
Stores day-wise leave records.
Useful for:
•	half-day leave 
•	mixed leave 
•	holiday overlap 
________________________________________
Fields
Field	Type
LeaveDetailID	BIGINT PK
LeaveApplicationID	BIGINT FK
LeaveDate	DATE
LeaveFraction	DECIMAL(4,2)
LeaveDayType	VARCHAR(20)
________________________________________
Examples
Leave Fraction
1.00
0.50
________________________________________
TABLE 5
LEV_LeaveAdjustment
Purpose:
Manual balance adjustment.
VERY IMPORTANT.
Used for:
•	management correction 
•	audit correction 
•	leave transfer 
________________________________________
Fields
Field	Type
AdjustmentID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
AdjustmentDate	DATE
AdjustmentDays	DECIMAL(6,2)
AdjustmentReason	VARCHAR(500)
ApprovedBy	BIGINT FK
CreatedDate	DATETIME
________________________________________
TABLE 6
LEV_LeaveOpeningBalance
Purpose:
Beginning-of-year leave allocation.
________________________________________
Fields
Field	Type
OpeningID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
LeaveYear	INT
OpeningDays	DECIMAL(6,2)
AllocationDate	DATE
________________________________________
TABLE 7
LEV_LeaveEncashment
Purpose:
Stores leave encashment.
________________________________________
Fields
Field	Type
EncashmentID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
EncashDays	DECIMAL(6,2)
EncashAmount	DECIMAL(18,2)
PayrollMonth	VARCHAR(7)
ApprovedBy	BIGINT FK
EncashDate	DATETIME
________________________________________
TABLE 8
LEV_LeaveAccrual
Purpose:
Stores monthly earned leave generation.
________________________________________
Fields
Field	Type
AccrualID	BIGINT PK
EmployeeID	BIGINT FK
LeaveTypeID	BIGINT FK
AccrualMonth	VARCHAR(7)
AccruedDays	DECIMAL(5,2)
GeneratedDate	DATETIME
________________________________________
TABLE 9
LEV_HolidayCalendar
Purpose:
Stores official holidays.
________________________________________
Fields
Field	Type
HolidayID	BIGINT PK
HolidayDate	DATE
HolidayName	VARCHAR(100)
HolidayType	VARCHAR(50)
UnitID	BIGINT FK
Status	BIT
________________________________________
Examples
•	Eid-ul-Fitr 
•	Eid-ul-Adha 
•	Independence Day 
•	Victory Day 
•	Factory Holiday 
________________________________________
TABLE 10
LEV_LeaveApprovalHistory
Purpose:
Tracks approval journey.
________________________________________
Fields
Field	Type
ApprovalHistoryID	BIGINT PK
LeaveApplicationID	BIGINT FK
WorkflowStepNo	INT
ApproverID	BIGINT FK
ActionTaken	VARCHAR(20)
ActionDate	DATETIME
Remarks	VARCHAR(500)
________________________________________
TABLE 11
LEV_LeaveCancellation
Purpose:
Tracks cancelled leaves.
________________________________________
Fields
Field	Type
CancellationID	BIGINT PK
LeaveApplicationID	BIGINT FK
CancellationDate	DATETIME
CancelledBy	BIGINT FK
Reason	VARCHAR(500)
ApprovedBy	BIGINT FK
________________________________________
TABLE 12
LEV_LeaveYear
Purpose:
Controls leave year.
________________________________________
Fields
Field	Type
LeaveYearID	BIGINT PK
LeaveYear	INT
StartDate	DATE
EndDate	DATE
IsCurrentYear	BIT
________________________________________
TABLE 13
LEV_LeavePolicy
VERY IMPORTANT
Purpose:
Stores leave rules.
________________________________________
Fields
Field	Type
PolicyID	BIGINT PK
LeaveTypeID	BIGINT FK
EmployeeCategoryID	BIGINT FK
AnnualEntitlement	DECIMAL(6,2)
CarryForwardAllowed	BIT
MaxCarryForwardDays	DECIMAL(6,2)
EncashAllowed	BIT
ApprovalLevels	INT
EffectiveDate	DATE
________________________________________
LEAVE RELATIONSHIP FLOW
HRM_EmployeeMaster
         │
         ▼
LEV_LeaveBalance
         │
         ▼
LEV_LeaveApplication
         │
         ▼
Workflow Engine
         │
         ▼
LEV_LeaveApprovalHistory
         │
         ▼
Attendance Engine
         │
         ▼
Payroll Engine
________________________________________
LEAVE TABLE SUMMARY
Table	Purpose
MST_LeaveType	Leave Master
LEV_LeaveBalance	Current Balance
LEV_LeaveApplication	Leave Request
LEV_LeaveApplicationDetails	Day-wise Details
LEV_LeaveAdjustment	Manual Adjustment
LEV_LeaveOpeningBalance	Opening Balance
LEV_LeaveEncashment	Encashment
LEV_LeaveAccrual	Monthly Accrual
LEV_HolidayCalendar	Holidays
LEV_LeaveApprovalHistory	Approval Audit
LEV_LeaveCancellation	Cancellation
LEV_LeaveYear	Leave Year Control
LEV_LeavePolicy	Leave Rules
Total Leave Tables = 13
________________________________________
IMPORTANT OBSERVATION FOR SYNEXIS
Your leave module is intentionally designed to support:
•	Bangladesh Labour Law requirements 
•	Textile factory operations 
•	Multi-company structure (NZ Denim, NZ Textile, NZ Fabrics) 
•	Carry-forward leave 
•	Leave encashment 
•	Half-day leave 
•	ESS leave application 
•	Workflow approval 
•	Payroll integration


     */
    internal class Class5
    {
    }
}
