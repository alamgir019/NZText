using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     43 PART 2 – ATTENDANCE DATABASE TABLES
________________________________________
ATTENDANCE DESIGN PHILOSOPHY
VERY IMPORTANT
Attendance must be separated into:
Biometric Punch
      ↓
ATT_RawPunch
      ↓
Attendance Engine
      ↓
ATT_ProcessedAttendance
      ↓
Payroll Engine
      ↓
Salary
Never allow payroll to read directly from biometric punches.
________________________________________
TABLE 1
ATT_RawPunch
Purpose:
Stores original biometric punches.
This table is:
IMMUTABLE
Meaning:
•	No Edit 
•	No Delete 
•	No Overwrite 
________________________________________
Fields
Field	Type
PunchID	BIGINT PK
EmployeeID	BIGINT FK
CardNo	VARCHAR(20)
PunchDateTime	DATETIME
PunchDate	DATE
PunchTime	TIME
DeviceID	BIGINT FK
DeviceLocation	VARCHAR(100)
VerificationMode	VARCHAR(50)
PunchSource	VARCHAR(50)
ImportBatchID	BIGINT
PunchStatus	VARCHAR(20)
CreatedDate	DATETIME
________________________________________
Example
Punch
05:58 IN
14:01 OUT
14:03 OT-IN
18:01 OT-OUT
________________________________________
TABLE 2
ATT_DeviceMaster
Purpose:
Stores biometric devices.
________________________________________
Fields
Field	Type
DeviceID	BIGINT PK
DeviceCode	VARCHAR(20)
DeviceName	VARCHAR(100)
IPAddress	VARCHAR(50)
Location	VARCHAR(100)
UnitID	BIGINT FK
Status	BIT
LastSyncTime	DATETIME
________________________________________
TABLE 3
ATT_DeviceSyncLog
Purpose:
Tracks synchronization.
________________________________________
Fields
Field	Type
SyncID	BIGINT PK
DeviceID	BIGINT FK
SyncStartTime	DATETIME
SyncEndTime	DATETIME
PunchCount	INT
SyncStatus	VARCHAR(20)
ErrorMessage	VARCHAR(500)
________________________________________
TABLE 4
ATT_ShiftRoster
Purpose:
Assigns actual working shift.
VERY IMPORTANT.
Supports:
•	Permanent shift 
•	Temporary shift 
•	Shift replacement 
________________________________________
Fields
Field	Type
RosterID	BIGINT PK
EmployeeID	BIGINT FK
ShiftID	BIGINT FK
RosterDate	DATE
AssignedBy	BIGINT FK
Reason	VARCHAR(200)
Status	BIT
________________________________________
Example
Normally:
B Shift
Today:
Assigned A Shift
This table handles that.
________________________________________
TABLE 5
ATT_OTAuthorization
Purpose:
Stores approved OT.
________________________________________
Fields
Field	Type
OTAuthID	BIGINT PK
EmployeeID	BIGINT FK
OTDate	DATE
ApprovedStartTime	TIME
ApprovedEndTime	TIME
ApprovedHours	DECIMAL(5,2)
RequestedBy	BIGINT FK
ApprovedBy	BIGINT FK
ApprovalDate	DATETIME
Status	VARCHAR(20)
________________________________________
Example
Employee	Approved
EMP001	14:00–18:00
EMP002	14:00–20:00
________________________________________
TABLE 6
ATT_ProcessedAttendance
MOST IMPORTANT TABLE
Payroll reads this table.
________________________________________
Fields
Field	Type
AttendanceID	BIGINT PK
EmployeeID	BIGINT FK
AttendanceDate	DATE
ShiftID	BIGINT FK
ActualInTime	DATETIME
ActualOutTime	DATETIME
PayableInTime	DATETIME
PayableOutTime	DATETIME
WorkedHours	DECIMAL(6,2)
OTWorkedHours	DECIMAL(6,2)
OTPayableHours	DECIMAL(6,2)
AttendanceStatus	VARCHAR(20)
ProcessingStatus	VARCHAR(20)
ProcessedDate	DATETIME
________________________________________
Examples
AttendanceStatus:
•	Present 
•	Late 
•	Absent 
•	Leave 
•	Holiday 
•	Weekly Off 
•	Half Day 
________________________________________
TABLE 7
ATT_AttendanceException
Purpose:
Stores anomalies.
________________________________________
Fields
Field	Type
ExceptionID	BIGINT PK
EmployeeID	BIGINT FK
AttendanceDate	DATE
ExceptionType	VARCHAR(50)
Severity	VARCHAR(20)
Remarks	VARCHAR(500)
ResolvedFlag	BIT
ResolvedBy	BIGINT FK
________________________________________
Examples
•	Missing IN 
•	Missing OUT 
•	OT Without Approval 
•	Duplicate Punch 
•	Excess Presence 
________________________________________
TABLE 8
ATT_AttendanceAdjustment
Purpose:
Controlled attendance correction.
VERY IMPORTANT.
Never modify processed attendance directly.
Adjustment must go through workflow.
________________________________________
Fields
Field	Type
AdjustmentID	BIGINT PK
EmployeeID	BIGINT FK
AttendanceDate	DATE
AdjustmentType	VARCHAR(50)
OldValue	VARCHAR(100)
NewValue	VARCHAR(100)
Reason	VARCHAR(500)
RequestedBy	BIGINT FK
ApprovedBy	BIGINT FK
ApprovalDate	DATETIME
Status	VARCHAR(20)
________________________________________
TABLE 9
ATT_AttendanceLock
Purpose:
Locks attendance after payroll.
________________________________________
Fields
Field	Type
LockID	BIGINT PK
AttendanceMonth	VARCHAR(7)
UnitID	BIGINT FK
LockDate	DATETIME
LockedBy	BIGINT FK
UnlockDate	DATETIME
UnlockedBy	BIGINT FK
Status	VARCHAR(20)
________________________________________
Example
2026-06
Locked
No changes allowed.
________________________________________
TABLE 10
ATT_ProcessingLog
Purpose:
Attendance engine activity log.
________________________________________
Fields
Field	Type
ProcessLogID	BIGINT PK
ProcessDate	DATE
TotalEmployees	INT
TotalProcessed	INT
TotalExceptions	INT
StartTime	DATETIME
EndTime	DATETIME
ProcessedBy	BIGINT FK
________________________________________
TABLE 11
ATT_InsideFactoryStatus
Purpose:
Real-time occupancy.
Useful for:
•	Security 
•	Emergency evacuation 
•	Fire drill 
•	Management dashboard 
________________________________________
Fields
Field	Type
StatusID	BIGINT PK
EmployeeID	BIGINT FK
LastPunchTime	DATETIME
CurrentStatus	VARCHAR(20)
UpdatedDate	DATETIME
________________________________________
Values
CurrentStatus:
•	Inside 
•	Outside 
________________________________________
TABLE 12
ATT_HolidayCalendar
Purpose:
Factory holidays.
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
TABLE 13
ATT_WeeklyOffPattern
Purpose:
Weekly off management.
________________________________________
Fields
Field	Type
WeeklyOffID	BIGINT PK
EmployeeID	BIGINT FK
DayOfWeek	VARCHAR(20)
EffectiveDate	DATE
Status	BIT
________________________________________
ATTENDANCE RELATIONSHIP MAP
ATT_DeviceMaster
       │
       ▼
ATT_RawPunch
       │
       ▼
ATT_ProcessedAttendance
       │
       ▼
Payroll

ATT_ShiftRoster
       │
       ▼

ATT_OTAuthorization
       │
       ▼

ATT_AttendanceException

ATT_AttendanceAdjustment

ATT_AttendanceLock
________________________________________
SYNEXIS ATTENDANCE TABLE COUNT
Table	Purpose
ATT_RawPunch	Evidence
ATT_DeviceMaster	Devices
ATT_DeviceSyncLog	Sync
ATT_ShiftRoster	Shift Assignment
ATT_OTAuthorization	OT
ATT_ProcessedAttendance	Payroll Attendance
ATT_AttendanceException	Exceptions
ATT_AttendanceAdjustment	Corrections
ATT_AttendanceLock	Locking
ATT_ProcessingLog	Engine Log
ATT_InsideFactoryStatus	Occupancy
ATT_HolidayCalendar	Holidays
ATT_WeeklyOffPattern	Weekly Off
Total Attendance Tables = 13
________________________________________
MY ERP ARCHITECT RECOMMENDATION
Before we proceed to Leave or Payroll, your developers should immediately review:
1.	HR Tables (Part 1) 
2.	Attendance Tables (Part 2) 
Together these two parts form nearly 50% of the core ERP foundation.
The next logical document should be:
DOCUMENT 43C
LEAVE DATABASE DICTIONARY
because Leave feeds Attendance, and Attendance feeds Payroll.
what is attendance and payroll engine look like
This is an excellent question.
Many ERP owners think the Attendance Engine and Payroll Engine are screens. They are not.
They are actually:
the brain of the ERP.
The screens are only for users.
The engines are background logic modules that run automatically.
________________________________________
1. ATTENDANCE ENGINE
Think of it like this:
Biometric Punch
       ↓
Attendance Engine
       ↓
Processed Attendance
Example:
Employee punches:
05:57 IN
14:02 OUT
The Attendance Engine reads:
ATT_RawPunch
and applies rules:
•	Shift = A 
•	Shift Start = 06:00 
•	Shift End = 14:00 
•	Early punch trim 
•	Late punch check 
•	Missing punch check 
•	OT validation 
Then writes:
ATT_ProcessedAttendance
Result:
Item	Value
Payable In	06:00
Payable Out	14:00
Status	Present
Worked Hours	8.00
OT	0
________________________________________
ATTENDANCE ENGINE ARCHITECTURE
ATT_RawPunch
      ↓
Shift Rule Module
      ↓
Missing Punch Module
      ↓
OT Validation Module
      ↓
Attendance Status Module
      ↓
Attendance Exception Module
      ↓
ATT_ProcessedAttendance
________________________________________
ATT-SCR-003 IS THE CONTROL PANEL
When you press:
PROCESS ATTENDANCE
you are actually calling:
Attendance Engine
which runs in the background.
________________________________________
ATTENDANCE ENGINE DASHBOARD
Developers should build:
Attendance Service
AttendanceEngine.cs
Inside:
ProcessAttendance()

AssignShift()

CalculateHours()

ValidateOT()

GenerateExceptions()

CreateProcessedAttendance()
________________________________________
2. PAYROLL ENGINE
Payroll Engine works exactly the same way.
It is NOT a screen.
It is a calculation brain.
________________________________________
PAYROLL FLOW
ATT_ProcessedAttendance
        ↓
Payroll Engine
        ↓
PAY_PayrollDetails
________________________________________
Example:
Employee:
Gross Salary:
20,000
Attendance:
30 days
OT:
42 hours
Loan:
1,000
Tax:
500
Payroll Engine calculates:
Gross Salary
+ OT
- Loan
- Tax
= Net Salary
________________________________________
PAYROLL ENGINE ARCHITECTURE
Attendance Reader
       ↓
Salary Structure Reader
       ↓
OT Calculator
       ↓
Leave Deduction Calculator
       ↓
Loan Recovery Calculator
       ↓
Tax Calculator
       ↓
Net Salary Calculator
       ↓
Payslip Generator
________________________________________
PAYROLL ENGINE MODULES
Developers should build:
PayrollEngine.cs
Functions:
GeneratePayroll()

CalculateSalary()

CalculateOT()

CalculateLeaveDeduction()

CalculateTax()

CalculateLoanRecovery()

GeneratePayslip()

LockPayroll()
________________________________________
PAYROLL PROCESSING SCREEN
Screen:
PAY-SCR-001
Payroll Processing
User presses:
PROCESS PAYROLL
Then:
Payroll Engine Runs
in background.
________________________________________
HOW THEY CONNECT
Biometric Device
        ↓
ATT_RawPunch
        ↓
Attendance Engine
        ↓
ATT_ProcessedAttendance
        ↓
Payroll Engine
        ↓
PAY_PayrollDetails
        ↓
PAY_Payslip
This is the most important flow in the entire ERP.
________________________________________
WHAT YOUR DEVELOPERS SHOULD BUILD
For SYNEXIS, I recommend two separate services:
Service 1
Attendance Engine Service
Responsibilities:
•	shift assignment 
•	punch analysis 
•	OT validation 
•	exception generation 
•	attendance processing 
________________________________________
Service 2
Payroll Engine Service
Responsibilities:
•	salary calculation 
•	OT payment 
•	deductions 
•	tax 
•	loan recovery 
•	payslip generation


     */
    internal class Class4
    {
    }
}
