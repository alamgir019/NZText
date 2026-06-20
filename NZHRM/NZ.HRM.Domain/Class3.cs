using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     PART 1 – CORE HR & ORGANIZATION TABLES
This becomes the actual specification your developers will code.
________________________________________
DATABASE NAMING STANDARD
Prefix	Meaning
MST_	Master
HRM_	HR
ATT_	Attendance
PAY_	Payroll
LEV_	Leave
REC_	Recruitment
SEP_	Separation
DIS_	Disciplinary
LON_	Loan
COM_	Communication
WF_	Workflow
SEC_	Security
AUD_	Audit
HIS_	History
________________________________________
TABLE 1
MST_Group
Purpose:
Top company group.
Example:
NZ Tex Group
Field	Type	Size	Key	Null
GroupID	BIGINT	-	PK	No
GroupCode	VARCHAR	20	Unique	No
GroupName	VARCHAR	100	-	No
Status	BIT	-	-	No
CreatedDate	DATETIME	-	-	No
CreatedBy	BIGINT	-	FK	No
________________________________________
TABLE 2
MST_Unit
Purpose:
Company under group.
Examples:
•	NZ Denim 
•	NZ Textile 
•	NZ Fabrics 
Field	Type
UnitID	BIGINT PK
GroupID	BIGINT FK
UnitCode	VARCHAR(20)
UnitName	VARCHAR(100)
Status	BIT
CreatedDate	DATETIME
________________________________________
TABLE 3
MST_Subunit
Purpose:
Production Shed
Examples:
•	Denim Washing 
•	Finishing Shed 
•	Dyeing Shed 
Field	Type
SubunitID	BIGINT PK
UnitID	BIGINT FK
SubunitCode	VARCHAR(20)
SubunitName	VARCHAR(100)
Status	BIT
________________________________________
TABLE 4
MST_Department
Examples:
•	Production 
•	HR 
•	Accounts 
•	Commercial 
Field	Type
DepartmentID	BIGINT PK
SubunitID	BIGINT FK
DepartmentCode	VARCHAR(20)
DepartmentName	VARCHAR(100)
Status	BIT
________________________________________
TABLE 5
MST_Section
Examples:
•	Sewing 
•	Cutting 
•	Finishing 
Field	Type
SectionID	BIGINT PK
DepartmentID	BIGINT FK
SectionCode	VARCHAR(20)
SectionName	VARCHAR(100)
Status	BIT
________________________________________
TABLE 6
MST_Cell
Examples:
•	Line 1 
•	Line 2 
•	Cell A 
Field	Type
CellID	BIGINT PK
SectionID	BIGINT FK
CellCode	VARCHAR(20)
CellName	VARCHAR(100)
Status	BIT
________________________________________
TABLE 7
MST_Designation
Field	Type
DesignationID	BIGINT PK
DesignationCode	VARCHAR(20)
DesignationName	VARCHAR(100)
GradeID	BIGINT FK
OTEligible	BIT
Status	BIT
________________________________________
TABLE 8
MST_Grade
Field	Type
GradeID	BIGINT PK
GradeCode	VARCHAR(20)
GradeName	VARCHAR(100)
MinimumSalary	DECIMAL(18,2)
MaximumSalary	DECIMAL(18,2)
Status	BIT
________________________________________
TABLE 9
MST_Shift
Field	Type
ShiftID	BIGINT PK
ShiftCode	VARCHAR(10)
ShiftName	VARCHAR(50)
StartTime	TIME
EndTime	TIME
GraceMinutes	INT
FullDayHours	DECIMAL(5,2)
Status	BIT
Examples:
ShiftCode	Time
A	06:00-14:00
B	14:00-22:00
C	22:00-06:00
G1	08:00-17:00
G2	09:00-18:00
________________________________________
TABLE 10
MST_EmployeeCategory
Examples:
•	Worker 
•	Staff 
•	Officer 
•	Manager 
•	Director 
Field	Type
CategoryID	BIGINT PK
CategoryCode	VARCHAR(20)
CategoryName	VARCHAR(100)
OTEligible	BIT
Status	BIT
________________________________________
TABLE 11
HRM_EmployeeMaster
This is the MOST IMPORTANT table in the entire ERP.
________________________________________
PRIMARY INFORMATION
Field	Type
EmployeeID	BIGINT PK
EmployeeCode	VARCHAR(20) Unique
CardNo	VARCHAR(20)
EmployeeName	VARCHAR(100)
FatherName	VARCHAR(100)
MotherName	VARCHAR(100)
DateOfBirth	DATE
Gender	VARCHAR(10)
MaritalStatus	VARCHAR(20)
BloodGroup	VARCHAR(10)
________________________________________
CONTACT INFORMATION
Field	Type
MobileNo	VARCHAR(20)
AlternateMobile	VARCHAR(20)
Email	VARCHAR(100)
PresentAddress	VARCHAR(500)
PermanentAddress	VARCHAR(500)
________________________________________
ORGANIZATION INFORMATION
Field	Type
GroupID	BIGINT FK
UnitID	BIGINT FK
SubunitID	BIGINT FK
DepartmentID	BIGINT FK
SectionID	BIGINT FK
CellID	BIGINT FK
DesignationID	BIGINT FK
GradeID	BIGINT FK
CategoryID	BIGINT FK
ShiftID	BIGINT FK
________________________________________
EMPLOYMENT INFORMATION
Field	Type
JoiningDate	DATE
ConfirmationDate	DATE
EmploymentStatus	VARCHAR(20)
ReportingManagerID	BIGINT FK
ProbationEndDate	DATE
________________________________________
PAYROLL INFORMATION
Field	Type
BasicSalary	DECIMAL(18,2)
GrossSalary	DECIMAL(18,2)
BankAccountNo	VARCHAR(50)
BankName	VARCHAR(100)
TINNo	VARCHAR(50)
________________________________________
SYSTEM INFORMATION
Field	Type
ActiveFlag	BIT
CreatedDate	DATETIME
CreatedBy	BIGINT
ModifiedDate	DATETIME
ModifiedBy	BIGINT
________________________________________
TABLE 12
HRM_EmployeeDocuments
Field	Type
DocumentID	BIGINT PK
EmployeeID	BIGINT FK
DocumentType	VARCHAR(50)
FileName	VARCHAR(500)
FilePath	VARCHAR(1000)
UploadDate	DATETIME
Examples:
•	NID 
•	Passport 
•	Certificate 
•	Appointment Letter 
•	Contract 
•	Photograph 
________________________________________
TABLE 13
HRM_EmployeeNominee
Field	Type
NomineeID	BIGINT PK
EmployeeID	BIGINT FK
NomineeName	VARCHAR(100)
Relationship	VARCHAR(50)
Percentage	DECIMAL(5,2)
MobileNo	VARCHAR(20)
________________________________________
RELATIONSHIP DIAGRAM
MST_Group
    │
    ▼
MST_Unit
    │
    ▼
MST_Subunit
    │
    ▼
MST_Department
    │
    ▼
MST_Section
    │
    ▼
MST_Cell

HRM_EmployeeMaster
     │
     ├── MST_Shift
     ├── MST_Designation
     ├── MST_Grade
     ├── MST_EmployeeCategory
     ├── HRM_EmployeeDocuments
     └── HRM_EmployeeNominee
________________________________________
MY RECOMMENDATION
Do not jump to Attendance tables yet.
Let us build the Database Dictionary systematically.
Sequence:
Part 1 ✅ Organization & HR Masters (completed above)
Part 2 → Attendance Tables (Raw Punch, Processed Attendance, OT Authorization, Shift Roster, Attendance Lock)
Part 3 → Leave Tables
Part 4 → Payroll Tables
Part 5 → Workflow Tables
Part 6 → Security & Audit Tables
This will ultimately become the actual database blueprint from which your developers can start coding immediately.





















What each part will contain
PART 1 — Organization & HR Masters
(Already started)
Expected tables:
•	MST_Group 
•	MST_Unit 
•	MST_Subunit 
•	MST_Department 
•	MST_Section 
•	MST_Cell 
•	MST_Designation 
•	MST_Grade 
•	MST_Shift 
•	MST_EmployeeCategory 
•	HRM_EmployeeMaster 
•	HRM_EmployeeDocuments 
•	HRM_EmployeeNominee 
•	HRM_EmployeeBank 
•	HRM_EmployeeEducation 
•	HRM_EmployeeExperience 
Approximately:
15–20 tables
________________________________________
PART 2 — Attendance Database
Expected tables:
•	ATT_RawPunch 
•	ATT_ProcessedAttendance 
•	ATT_ShiftRoster 
•	ATT_OTAuthorization 
•	ATT_AttendanceAdjustment 
•	ATT_AttendanceLock 
•	ATT_AttendanceException 
•	ATT_DeviceMaster 
•	ATT_DeviceSyncLog 
•	ATT_ProcessingLog 
Approximately:
10–15 tables
This is one of the most critical areas because payroll depends on it.
________________________________________
PART 3 — Leave Database
Expected tables:
•	LEV_Application 
•	LEV_Balance 
•	LEV_OpeningBalance 
•	LEV_Adjustment 
•	LEV_Encashment 
•	LEV_LeaveType 
•	LEV_HolidayCalendar 
•	LEV_WeeklyOffPattern 
Approximately:
8–12 tables
________________________________________
PART 4 — Payroll Database
Expected tables:
•	PAY_SalaryStructure 
•	PAY_PayrollHeader 
•	PAY_PayrollDetails 
•	PAY_OTDetails 
•	PAY_Deduction 
•	PAY_Arrear 
•	PAY_Bonus 
•	PAY_Increment 
•	PAY_Payslip 
•	PAY_Tax 
•	PAY_PayrollLock 
Approximately:
15–20 tables
This is the heart of SYNEXIS.
________________________________________
PART 5 — Workflow Database
Expected tables:
•	WF_WorkflowMaster 
•	WF_WorkflowStep 
•	WF_ApprovalHistory 
•	WF_PendingApproval 
•	WF_Delegation 
•	WF_Notification 
Approximately:
6–10 tables
This single engine will drive:
•	Leave 
•	OT 
•	Attendance Adjustment 
•	Loan 
•	Recruitment 
•	Promotion 
•	Separation 
________________________________________
PART 6 — Security & Audit Database
Expected tables:
•	SEC_User 
•	SEC_Role 
•	SEC_Permission 
•	SEC_UserRole 
•	SEC_RolePermission 
•	SEC_Session 
•	AUD_LoginHistory 
•	AUD_DataChange 
•	AUD_ApprovalTrail 
•	AUD_ReportAccess 
•	AUD_ExportHistory 
Approximately:
10–15 tables
________________________________________
Additional Modules
After Part 1–6, we shall add some specialized modules:
Recruitment
•	REC_Requisition 
•	REC_Candidate 
•	REC_Interview 
•	REC_Appointment 
Loan
•	LON_LoanMaster 
•	LON_Installment 
•	LON_Recovery 
Disciplinary
•	DIS_Incident 
•	DIS_ShowCause 
•	DIS_Inquiry 
Separation
•	SEP_Separation 
•	SEP_Clearance 
•	SEP_Settlement 
ESS
•	ESS_ProfileRequest 
•	ESS_LoginHistory 
Communication
•	COM_Notification 
•	COM_SMSQueue 
•	COM_EmailQueue 
________________________________________
Final Estimate
For a textile ERP of the size of SYNEXIS:
Area	Approx Tables
HR Masters	20
Attendance	15
Leave	10
Payroll	20
Workflow	10
Security & Audit	15
Recruitment	8
Loan	6
Disciplinary	8
Separation	6
ESS	6
Communication	6
Total ≈ 120–140 tables
That is completely normal for an enterprise-grade ERP.
________________________________________
What I would do if I were your ERP Architect
I would now create:
Document 43A
Organization & HR Tables
Document 43B
Attendance Tables
Document 43C
Leave Tables
Document 43D
Payroll Tables
Document 43E
Workflow Tables
Document 43F
Security & Audit Tables
These six documents will become the actual coding foundation for your developers.
My recommendation is:
stop creating more UI screens for now and complete the Database Dictionary first.
Once the tables are finalized, the screen fields, APIs, reports, and workflows become much easier and more accurate.


     */
    internal class Class3
    {
    }
}
