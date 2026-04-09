using Microsoft.VisualBasic;
using Microsoft.Win32;
using NZ.HRM.Domain.Entities;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System;

namespace NZ.HRM.Domain;

/*
1.	Company ID Company Name and Location
 Location – Vulta, Rupgonj, Narayangonj
01-	NZ Textile Ltd 
02-	 NZ Spinning Ltd
03-	NZ Dy Flax Ltd
04-	NZ Fabrics Ltd
05-	NZ Denim Ltd.
Location – Araihazar, Narayangonj
        06 - NZ Apparels Ltd
When the user logs in, they will select the company.Then all data will automatically belong to that company:
Employees
Attendance
Payroll
Leave
Production workers
Compliance records
So one ERP – but data separated by company.
2. 	What are the changes Between the Companies
Only a few things will differ:
Company name
Factory location
Employee list
Salary structure
Production units
Attendance devices
Bank accounts
Everything else remains the same ERP structure.
3.	User Types- The ERP will have different categories of users:

a.Top Management - 	CEO / Directors 	-	Access: Dashboard, reports only.
b.HR Department   -	HR Manager/ HR Officers-	Access: Full HR control
c.	Audit Departmrnt -	Access: Payroll, salary reports.
d.Production / Supervisors -	Section Supervisors/ Department Heads - Access: Attendance view /Leave approval
e.System Administrator - 	IT Department - Access: all User control and System configuration
         f.Employee : some sort of employees should have access to apply for leave























Part - 3
1.	Modules of the ERP
a.Organization Setup Module
Company
Location
Unit / Factory
Department
Section
Designation
Grade
Shift
Holiday
b. 	Employee Master Module
Employee registration
Personal information
Job details
Salary setup
Document storage
c. 	Attendance Management Module
Biometric data import
Shift management
Late/early calculation
Absence tracking
d.Leave Management Module
Leave types (CL, SL, EL, etc.)
Leave application
Approval workflow
Leave balance
e. 	Overtime Management Module
OT eligibility
OT hours calculation
OT approval
OT summary
f.Payroll Management Module
Salary structure
Monthly payroll processing
OT integration
Deductions (leave, loans, etc.)
Payslip generation
Bank transfer file

g.Compliance Module
Labour law registers
Continuous service tracking
Earned leave (240 days rule)
Wage register
Overtime register
h.Recruitment Module
Job requisition
Applicant tracking
Interview records
Hiring approval
i.Training Module
Training programs
Attendance
Evaluation
j.Performance Management Module
Employee evaluation
Supervisor ratings
Promotion recommendations
k.Document Management Module
Appointment letters
Promotion letters
Warning letters
Employee file archive
l.	 Dashboard & Analytics Module
Total manpower
Attendance status
Overtime analysis
Labour cost
Department-wise reports
m.System Administration Module
User management
Role management
Permission setup
System logs
Backup

2.	Development Phases - ERP will be developed in phases, not all at once.

Phase 1 	– 	Foundation
Organization Setup
Employee Master
Phase 2 – 	Attendance
Attendance import
Shift management
Reports
Phase 3 – 	Leave & Overtime
Leave system
OT calculation

Phase 4 – 	Payroll
Salary structure
Payroll processing
Payslip
Phase 5 – 	Compliance
Labour law reports
Registers
Phase 6 – 	Advanced Modules
Recruitment
Training
Performance
Phase 7 – 	Dashboard & Analytics
Management reports
CEO dashboard
We can go live after Phase 4

Part -4

HRM ERP – FINAL TABLE MASTER LIST
(Including Labour Law 2006 + All Modules)
________________________________________
🧩 1️⃣ ORGANIZATION SETUP(MASTER TABLES)
Company
Location
Unit
Department
Section
Designation
Grade
Shift
Holiday
Employment_Type
________________________________________
👤 2️⃣ EMPLOYEE MASTER(CORE TABLES)
Employee_Master
Employee_Personal
Employee_Job
Employee_Salary
Employee_Photo
Employee_Document
________________________________________
🏭 3️⃣ EMPLOYMENT LIFECYCLE
Appointment
Confirmation
Transfer_History
Promotion_History
Employment_History
________________________________________
⚖️ 4️⃣ SEPARATION / EXIT(LABOUR LAW)
Separation_Master
Separation_Detail
Types covered:
•	Resignation 
•	Termination 
•	Dismissal 
•	Retrenchment
________________________________________
📅 5️⃣ ATTENDANCE MODULE
Attendance_Log
Attendance
Attendance_Adjustment
Shift_Assignment
Attendance_Summary
Attendance_Compliance
________________________________________
🌴 6️⃣ LEAVE MANAGEMENT(EXTENDED – LABOUR LAW)
Leave_Type
Leave_Policy
Leave_Application
Leave_Approval
Leave_Balance
Leave_History
EL_Accrual
Maternity_Leave
Leave_Encashment
Covers:
•	CL(Casual Leave)
•	SL(Sick Leave)
•	EL(Earned Leave – 240 days rule)
•	ML(Maternity Leave)
________________________________________
⏱️ 7️⃣ OVERTIME MODULE
OT_Eligibility
Overtime_Record
OT_Approval
OT_Summary
________________________________________
💰 8️⃣ PAYROLL MODULE
Salary_Structure
Employee_Salary
Payroll_Master
Payroll_Detail
Payroll_Deduction
Payslip
Bank_Transfer
Bonus_Record
________________________________________
💳 9️⃣ LOAN MANAGEMENT
Loan_Master
Loan_Recovery
Loan_Installment
________________________________________
📈 🔟 INCREMENT MANAGEMENT
Increment_Policy
Increment_Record
Increment_Approval
________________________________________
⚖️ 1️⃣1️⃣ COMPLIANCE(LABOUR LAW REGISTERS)
Worker_Register
Wage_Register
Leave_Register
OT_Register
Service_Continuity
EL_Calculation
Gratuity_Record
________________________________________
🚨 1️⃣2️⃣ DISCIPLINARY MODULE
Disciplinary_Action
Show_Cause
Charge_Sheet
Inquiry_Record
________________________________________
📥 1️⃣3️⃣ RECRUITMENT MODULE
Job_Requisition
Applicant
Interview
Selection
Offer_Letter
________________________________________
🎓 1️⃣4️⃣ TRAINING MODULE
Training_Program
Training_Schedule
Training_Attendance
Training_Evaluation
________________________________________
📊 1️⃣5️⃣ PERFORMANCE MANAGEMENT
Performance_Criteria
Employee_Performance
Promotion_Record
________________________________________
📄 1️⃣6️⃣ DOCUMENT MANAGEMENT
Document_Type
Employee_Document
Letter_Generation
________________________________________
🔐 1️⃣7️⃣ SYSTEM ADMINISTRATION
Users
Roles
Permissions
User_Role
System_Log
Backup_Log
________________________________________
🔄 1️⃣8️⃣ APPROVAL WORKFLOW(CROSS-MODULE)
Approval_Workflow
Approval_Transaction
Approval_History
________________________________________
📊 1️⃣9️⃣ DASHBOARD / ANALYTICS
Dashboard_Snapshot(optional)



Above tables have been grouped into 5 Packs( 1 to 5). See Details


PACK-1: CORE ERP TABLES(FULL DETAILS)
________________________________________
1️⃣ Company
Fields
Field Type    Example
Company_ID  INT(PK)    1
Company_Code VARCHAR(10) NZD
Company_Name    VARCHAR(100)    NZ Denim Ltd
Location_ID INT	1
Active_Status BIT 1
________________________________________
Screen: Company Setup
•	Add Company
•	Edit Company
•	Dropdown: Location
________________________________________
2️⃣ Location
Field   Type Example
Location_ID INT 1
Location_Name VARCHAR Vulta
District    VARCHAR Narayanganj
________________________________________
Screen
•	Add Location
•	List View 
________________________________________
3️⃣ Unit
Field	Type	Example
Unit_ID	INT	1
Company_ID	INT	5
Unit_Name	VARCHAR	Denim Unit
Location_ID	INT	1
________________________________________
Screen
•	Unit Setup 
•	Dropdown: Company 
________________________________________
4️⃣ Department
Field	Type	Example
Department_ID	INT	1
Department_Name	VARCHAR	Dyeing
Department_Code	VARCHAR	DYE
________________________________________
Screen
•	Department Setup 
________________________________________
5️⃣ Section
Field	Type	Example
Section_ID	INT	1
Department_ID	INT	1
Section_Name	VARCHAR	Dyeing Line 1
________________________________________
Screen
•	Section Setup 
•	Filter by Department 
________________________________________
6️⃣ Designation 
Field	Type	Example
Designation_ID	INT	1
Designation_Name	VARCHAR	Operator
Level	INT	1
________________________________________
Screen
•	Designation Setup 
________________________________________
7️⃣ Grade
Field	Type	Example
Grade_ID	INT	1
Grade_Name	VARCHAR	G1
Min_Salary	DECIMAL	8000
Max_Salary	DECIMAL	12000
________________________________________
Screen
•	Grade Setup 
________________________________________
8️⃣ Shift
Field	Type	Example
Shift_ID	INT	1
Shift_Name	VARCHAR	Day
Start_Time	TIME	08:00
End_Time	TIME	17:00
________________________________________
Screen
•	Shift Setup 
________________________________________
9️⃣ Employee_Master  
Field	Type	Example
Employee_ID	INT	1001
Employee_Code	VARCHAR	ND-001
Company_ID	INT	5
Unit_ID	INT	1
Department_ID	INT	2
Section_ID	INT	1
Designation_ID	INT	1
Grade_ID	INT	1
Joining_Date	DATE	01-01-2024
Status	VARCHAR	Active
________________________________________
Screen: Employee Entry
•	Full form (we already designed) 
•	Dropdowns from master tables 
________________________________________
🔟 Employee_Personal
Field	Type	Example
Employee_ID	INT	1001
Name	VARCHAR	Rahim
Father_Name	VARCHAR	Karim
DOB	DATE	01-01-1995
NID	VARCHAR	123456
________________________________________
Screen
•	Tab inside Employee Entry 
________________________________________
1️⃣1️⃣ Employee_Salary
Field	Type	Example
Employee_ID	INT	1001
Basic	DECIMAL	9000
House_Rent	DECIMAL	4500
Medical	DECIMAL	1000
Gross	DECIMAL	14500
________________________________________
Screen
•	Salary tab 
•	Auto calculate Gross 
________________________________________
1️⃣2️⃣ Users
Field	Type	Example
User_ID	INT	1
Username	VARCHAR	hr_admin
Password	VARCHAR	*****
Role_ID	INT	1
________________________________________
Screen
•	User Creation 
________________________________________
1️⃣3️⃣ Roles
Field	Type	Example
Role_ID	INT	1
Role_Name	VARCHAR	HR
________________________________________
Screen
•	Role Setup 
________________________________________
1️⃣4️⃣ Permissions
Field	Type	Example
Permission_ID	INT	1
Role_ID	INT	1
Module	VARCHAR	Employee
Access_Type	VARCHAR	Full
________________________________________
Screen
•	Permission Setup 
________________________________________
1️⃣5️⃣ Approval_Workflow
Field	Type	Example
Workflow_ID	INT	1
Module	VARCHAR	Leave
Level	INT	1
Role	VARCHAR	Supervisor
________________________________________
Screen
•	Workflow Setup 









PACK-2: ATTENDANCE + LEAVE + OT (FULL DETAILS)
📘 1️⃣ Attendance_Log (Raw Machine Data)
Fields
Field	Type	Example
Log_ID	INT (PK)	1
Employee_ID	INT	1001
Machine_ID	VARCHAR(20)	M01
Log_Date	DATE	01-03-2026
Log_Time	TIME	08:01
In_Out	VARCHAR(5)	IN
________________________________________
Screen: Attendance Import
•	Upload from machine 
•	View raw logs 
________________________________________
📘 2️⃣ Attendance (Processed Data)
Fields
Field	Type	Example
Attendance_ID	INT	1
Employee_ID	INT	1001
Date	DATE	01-03-2026
In_Time	TIME	08:01
Out_Time	TIME	18:05
Work_Hours	DECIMAL	9
Status	VARCHAR	Present
Late_Minutes	INT	5
________________________________________
Screen: Attendance View
•	Daily attendance 
•	Filter by department 
________________________________________
📘 3️⃣ Attendance_Adjustment
Fields
Field	Type	Example
Adjustment_ID	INT	1
Employee_ID	INT	1001
Date	DATE	01-03-2026
Old_In	TIME	NULL
New_In	TIME	08:00
Reason	VARCHAR	Missed punch
Approved_By	INT	2
________________________________________
Screen: Attendance Correction
•	Edit attendance 
•	Approval required 
________________________________________
📘 4️⃣ Shift_Assignment
Fields
Field	Type	Example
Assignment_ID	INT	1
Employee_ID	INT	1001
Shift_ID	INT	1
Effective_From	DATE	01-03-2026
________________________________________
Screen: Shift Assignment
•	Assign shift to employee 
•	Bulk assignment 
________________________________________
📘 5️⃣ Attendance_Summary
Fields
Field	Type	Example
Employee_ID	INT	1001
Month	VARCHAR	Mar-2026
Present_Days	INT	26
Absent_Days	INT	2
Late_Count	INT	3
________________________________________
Screen: Monthly Summary
•	Department-wise summary 
•	Export to Excel 
________________________________________
📘 6️⃣ Attendance_Compliance
(For Labour Law)
Fields
Field	Type	Example
Employee_ID	INT	1001
Month	VARCHAR	Mar-2026
Days_Worked	INT	26
Weekly_Holidays	INT	4
OT_Hours	DECIMAL	40
________________________________________
Screen
•	Compliance report 
•	Labour inspection use 
________________________________________
📘 7️⃣ Leave_Type (Master)
Fields
Field	Type	Example
Leave_Type_ID	INT	1
Leave_Name	VARCHAR	Casual
Max_Days	INT	10
Carry_Forward	BIT	0
________________________________________
Screen: Leave Type Setup
•	Define CL, SL, EL, ML 
________________________________________
📘 8️⃣ Leave_Policy
Fields
Field	Type	Example
Policy_ID	INT	1
Leave_Type_ID	INT	3
Rule	VARCHAR	After 240 days
Max_Per_Year	INT	14
________________________________________
Screen
•	Define rules (EL, ML etc.) 
________________________________________
📘 9️⃣ Leave_Application
Fields
Field	Type	Example
Leave_ID	INT	1
Employee_ID	INT	1001
Leave_Type_ID	INT	1
Start_Date	DATE	05-03-2026
End_Date	DATE	06-03-2026
Reason	VARCHAR	Personal
Status	VARCHAR	Pending
________________________________________
Screen: Apply Leave
•	Employee submits leave 
________________________________________
📘 🔟 Leave_Approval
Fields
Field	Type	Example
Approval_ID	INT	1
Leave_ID	INT	1
Approved_By	INT	2
Level	INT	1
Status	VARCHAR	Approved
________________________________________
Screen: Leave Approval
•	Supervisor approval 
•	HR approval 
________________________________________
📘 1️⃣1️⃣ Leave_Balance
Fields
Field	Type	Example
Employee_ID	INT	1001
Leave_Type_ID	INT	1
Balance	INT	8
________________________________________
Screen: Leave Balance
•	View remaining leave 
________________________________________
📘 1️⃣2️⃣ Leave_History
Fields
Field	Type	Example
Employee_ID	INT	1001
Leave_ID	INT	1
Date	DATE	05-03-2026
________________________________________
Screen
•	Leave history 
________________________________________
📘 1️⃣3️⃣ EL_Accrual
Fields
Field	Type	Example
Employee_ID	INT	1001
Days_Worked	INT	240
EL_Earned	INT	14
________________________________________
Screen
•	EL calculation report 
________________________________________
📘 1️⃣4️⃣ Maternity_Leave
Fields
Field	Type	Example
Employee_ID	INT	2001
Start_Date	DATE	01-03-2026
End_Date	DATE	01-06-2026
Benefit_Paid	DECIMAL	30000
________________________________________
Screen
•	Maternity leave tracking 
________________________________________
📘 1️⃣5️⃣ Leave_Encashment
Fields
Field	Type	Example
Employee_ID	INT	1001
Leave_Type	VARCHAR	EL
Days	INT	10
Amount	DECIMAL	5000
________________________________________
Screen
•	Leave encashment 
________________________________________
📘 1️⃣6️⃣ OT_Eligibility
Fields
Field	Type	Example
Employee_ID	INT	1001
Eligible	BIT	1
Max_Hours	INT	4
________________________________________
Screen
•	Set OT eligibility 
________________________________________
📘 1️⃣7️⃣ Overtime_Record
Fields
Field	Type	Example
OT_ID	INT	1
Employee_ID	INT	1001
Date	DATE	02-03-2026
Hours	DECIMAL	2
________________________________________
Screen
•	OT entry 
________________________________________
📘 1️⃣8️⃣ OT_Approval
Fields
Field	Type	Example
OT_ID	INT	1
Approved_By	INT	2
Status	VARCHAR	Approved
________________________________________
Screen
•	OT approval 
________________________________________
📘 1️⃣9️⃣ OT_Summary
Fields
Field	Type	Example
Employee_ID	INT	1001
Month	VARCHAR	Mar-2026
Total_OT	DECIMAL	40
________________________________________
Screen
•	OT summary 

PACK-2 SUMMARY
Attendance → Leave → Overtime → Compliance

🧠 CRITICAL INSIGHT
This pack connects:
Attendance → Leave → OT → Payroll
👉 If Pack-2 is correct, Payroll will be easy.


































PACK-3: PAYROLL + INCREMENT + LOAN (FULL DETAILS)

💰 1️⃣ Salary_Structure (Master)
Fields
Field	Type	Example
Structure_ID	INT (PK)	1
Component_Name	VARCHAR(50)	Basic
Type	VARCHAR(20)	Earning
Default_Value	DECIMAL	9000
________________________________________
Screen: Salary Structure Setup
•	Add components: 
o	Basic 
o	House Rent 
o	Medical 
o	Allowances 
________________________________________
💰 2️⃣ Employee_Salary (Extended)
(Already exists, now enhanced)
Fields
Field	Type	Example
Employee_ID	INT	1001
Basic	DECIMAL	9000
House_Rent	DECIMAL	4500
Medical	DECIMAL	1000
Gross	DECIMAL	14500
Effective_Date	DATE	01-01-2026
________________________________________
Screen: Employee Salary Setup
•	Assign salary 
•	Track history 
________________________________________
💰 3️⃣ Payroll_Master
Fields
Field	Type	Example
Payroll_ID	INT	1
Month	VARCHAR(10)	Feb-2026
Company_ID	INT	5
Processed_Date	DATE	28-02-2026
Status	VARCHAR	Completed
________________________________________
Screen: Payroll Processing
•	Select month 
•	Run payroll 
________________________________________
💰 4️⃣ Payroll_Detail
Fields
Field	Type	Example
Payroll_ID	INT	1
Employee_ID	INT	1001
Basic	DECIMAL	9000
OT_Amount	DECIMAL	1200
Leave_Deduction	DECIMAL	300
Loan_Deduction	DECIMAL	500
Net_Pay	DECIMAL	9400
________________________________________
Screen: Payroll Detail View
•	Employee-wise salary 
________________________________________
💰 5️⃣ Payroll_Deduction
Fields
Field	Type	Example
Deduction_ID	INT	1
Employee_ID	INT	1001
Type	VARCHAR	Loan
Amount	DECIMAL	500
________________________________________
Screen
•	Deduction setup 
________________________________________
💰 6️⃣ Payslip
Fields
Field	Type	Example
Payslip_ID	INT	1
Employee_ID	INT	1001
Month	VARCHAR	Feb-2026
Net_Pay	DECIMAL	9400
________________________________________
Screen: Payslip
•	Print / download 
________________________________________
💰 7️⃣ Bank_Transfer
Fields
Field	Type	Example
Transfer_ID	INT	1
Employee_ID	INT	1001
Bank_Account	VARCHAR	123456
Amount	DECIMAL	9400
________________________________________
Screen
•	Export bank file 
________________________________________
🎁 8️⃣ Bonus_Record
Fields
Field	Type	Example
Bonus_ID	INT	1
Employee_ID	INT	1001
Bonus_Type	VARCHAR	Eid Bonus
Amount	DECIMAL	7000
________________________________________
Screen
•	Bonus processing 
________________________________________
📈 9️⃣ Increment_Policy
Fields
Field	Type	Example
Policy_ID	INT	1
Policy_Name	VARCHAR	Annual
Type	VARCHAR	Percentage
Value	DECIMAL	10
________________________________________
Screen
•	Define increment rules 
________________________________________
📈 🔟 Increment_Record
Fields
Field	Type	Example
Increment_ID	INT	1
Employee_ID	INT	1001
Old_Salary	DECIMAL	12000
Increment_Value	DECIMAL	2000
New_Salary	DECIMAL	14000
Effective_Date	DATE	01-01-2026
________________________________________
Screen: Increment Entry
•	Apply increment 
________________________________________
📈 1️⃣1️⃣ Increment_Approval
Fields
Field	Type	Example
Increment_ID	INT	1
Approved_By	INT	2
Status	VARCHAR	Approved
________________________________________
Screen
•	Approve increment 
________________________________________
💳 1️⃣2️⃣ Loan_Master
Fields
Field	Type	Example
Loan_ID	INT	1
Employee_ID	INT	1001
Loan_Type	VARCHAR	Salary Advance
Amount	DECIMAL	10000
Start_Date	DATE	01-02-2026
________________________________________
Screen
•	Create loan 
________________________________________
💳 1️⃣3️⃣ Loan_Installment
Fields
Field	Type	Example
Installment_ID	INT	1
Loan_ID	INT	1
Month	VARCHAR	Mar-2026
Amount	DECIMAL	1000
________________________________________
Screen
•	Installment schedule 
________________________________________
💳 1️⃣4️⃣ Loan_Recovery
Fields
Field	Type	Example
Recovery_ID	INT	1
Employee_ID	INT	1001
Month	VARCHAR	Mar-2026
Recovered	DECIMAL	1000
________________________________________
Screen
•	Track recovery 
________________________________________
🎯 PACK-3 SUMMARY
Salary → Payroll → Bonus → Increment → Loan
________________________________________
🔗 VERY IMPORTANT FLOW
Attendance + OT + Leave
        ↓
Payroll Calculation
        ↓
Payslip + Bank Transfer
________________________________________
🧠 CRITICAL ERP LOGIC
👉 Payroll depends on:
•	Attendance (Pack-2) 
•	OT (Pack-2) 
•	Leave (Pack-2) 
•	Salary (Pack-3) 
________________________________________
After Pack-2:
1️⃣ Create payroll tables
2️⃣ Build payroll screen
3️⃣ Run test salary



































PACK-4: COMPLIANCE + SEPARATION + DISCIPLINE

⚖️ 1️⃣ Worker_Register
(Mandatory labour register)
Fields
Field	Type	Example
Register_ID	INT	1
Employee_ID	INT	1001
Name	VARCHAR	Rahim
Designation	VARCHAR	Operator
Joining_Date	DATE	01-01-2024
Department	VARCHAR	Dyeing
________________________________________
Screen
•	Worker Register Report 
•	Printable format 
________________________________________
⚖️ 2️⃣ Wage_Register
Fields
Field	Type	Example
Record_ID	INT	1
Employee_ID	INT	1001
Month	VARCHAR	Feb-2026
Basic	DECIMAL	9000
OT	DECIMAL	1200
Net	DECIMAL	9400
________________________________________
Screen
•	Wage Register 
•	Labour audit report 
________________________________________
⚖️ 3️⃣ Leave_Register
Fields
Field	Type	Example
Record_ID	INT	1
Employee_ID	INT	1001
Leave_Type	VARCHAR	EL
Days	INT	5
Month	VARCHAR	Feb-2026
________________________________________
Screen
•	Leave Register 


⚖️ 4️⃣ OT_Register
Fields
Field	Type	Example
Record_ID	INT	1
Employee_ID	INT	1001
Date	DATE	01-03-2026
OT_Hours	DECIMAL	2
________________________________________
Screen
•	OT Register 
________________________________________
⚖️ 5️⃣ Service_Continuity
(Very important for EL + gratuity)
Fields
Field	Type	Example
Employee_ID	INT	1001
Start_Date	DATE	01-01-2024
Days_Worked	INT	240
Continuous	BIT	1
________________________________________
Screen
•	Service record 
________________________________________
⚖️ 6️⃣ EL_Calculation
Fields
Field	Type	Example
Employee_ID	INT	1001
Days_Worked	INT	240
EL_Earned	INT	14
________________________________________
Screen
•	EL report 
________________________________________
⚖️ 7️⃣ Gratuity_Record
Fields
Field	Type	Example
Employee_ID	INT	1001
Years_Service	INT	5
Amount	DECIMAL	50000
________________________________________
Screen
•	Gratuity report 
________________________________________


🚪 8️⃣ Separation_Master
Fields
Field	Type	Example
Separation_ID	INT	1
Employee_ID	INT	1001
Type	VARCHAR	Resignation
Date	DATE	01-03-2026
________________________________________
Screen: Employee Exit
•	Select type: 
o	Resignation 
o	Termination 
o	Dismissal 
o	Retrenchment 
________________________________________
🚪 9️⃣ Separation_Detail
Fields
Field	Type	Example
Separation_ID	INT	1
Reason	VARCHAR	Personal
Notice_Period	INT	30
Final_Settlement	BIT	1
________________________________________
Screen
•	Exit details 
________________________________________
🚨 🔟 Disciplinary_Action
Fields
Field	Type	Example
Action_ID	INT	1
Employee_ID	INT	1001
Action_Type	VARCHAR	Warning
Date	DATE	01-02-2026
________________________________________
Screen
•	Disciplinary entry 
________________________________________
🚨 1️⃣1️⃣ Show_Cause
Fields
Field	Type	Example
ShowCause_ID	INT	1
Employee_ID	INT	1001
Issue	VARCHAR	Absenteeism
Date_Issued	DATE	01-02-2026
Reply_Date	DATE	05-02-2026
________________________________________
Screen
•	Issue show cause 
________________________________________
🚨 1️⃣2️⃣ Charge_Sheet
Fields
Field	Type	Example
Charge_ID	INT	1
Employee_ID	INT	1001
Charge	VARCHAR	Misconduct
Date	DATE	10-02-2026
________________________________________
Screen
•	Charge sheet 
________________________________________
🚨 1️⃣3️⃣ Inquiry_Record
Fields
Field	Type	Example
Inquiry_ID	INT	1
Employee_ID	INT	1001
Result	VARCHAR	Guilty
Decision	VARCHAR	Termination
________________________________________
Screen
•	Inquiry outcome 
________________________________________
🎯 PACK-4 SUMMARY
Compliance → Service → EL → Separation → Discipline
________________________________________
🔗 IMPORTANT LEGAL FLOW
Attendance → EL Eligibility → Leave → Payroll → Compliance Register
AND
Misconduct → Show Cause → Charge Sheet → Inquiry → Action
________________________________________
With this:
✅ Fully compliant
✅ Audit ready
✅ Buyer acceptable
________________________________________
After Pack-3:
1️⃣ Create compliance tables
2️⃣ Build separation screen
3️⃣ Build disciplinary workflow

PACK-5: ADVANCED HR + DOCUMENT + DASHBOARD
________________________________________
📥 1️⃣ Recruitment Module
________________________________________
🔹 Job_Requisition
Field	Type	Example
Requisition_ID	INT	1
Department_ID	INT	3
Required_No	INT	10
Requested_By	INT	2
Date	DATE	01-03-2026
Status	VARCHAR	Approved
________________________________________
Screen: Job Requisition
•	Department requests manpower 
•	Approval workflow 
________________________________________
🔹 Applicant
Field	Type	Example
Applicant_ID	INT	1
Name	VARCHAR	Rahim
Mobile	VARCHAR	017xxxx
Skill	VARCHAR	Operator
Applied_For	VARCHAR	Dyeing
________________________________________
Screen: Applicant Entry
•	Candidate registration 
________________________________________
🔹 Interview
Field	Type	Example
Interview_ID	INT	1
Applicant_ID	INT	1
Score	INT	80
Remarks	VARCHAR	Good
________________________________________
Screen: Interview Evaluation
________________________________________
🔹 Selection
Field	Type	Example
Selection_ID	INT	1
Applicant_ID	INT	1
Selected	BIT	1
Joining_Date	DATE	10-03-2026
________________________________________
Screen: Selection Approval
________________________________________
🔹 Offer_Letter
Field	Type	Example
Offer_ID	INT	1
Applicant_ID	INT	1
Salary	DECIMAL	12000
Issue_Date	DATE	05-03-2026
________________________________________
Screen: Offer Letter Generation
________________________________________
🎓 2️⃣ Training Module
________________________________________
🔹 Training_Program
Field	Type	Example
Program_ID	INT	1
Program_Name	VARCHAR	Safety Training
Type	VARCHAR	Compliance
________________________________________
🔹 Training_Schedule
Field	Type	Example
Schedule_ID	INT	1
Program_ID	INT	1
Date	DATE	10-03-2026
Trainer	VARCHAR	Mr. X
________________________________________
🔹 Training_Attendance
Field	Type	Example
Employee_ID	INT	1001
Schedule_ID	INT	1
Status	VARCHAR	Present
________________________________________
🔹 Training_Evaluation
Field	Type	Example
Employee_ID	INT	1001
Score	INT	85
Remarks	VARCHAR	Good
________________________________________
Screens
•	Training setup 
•	Training schedule 
•	Attendance 
•	Evaluation 
________________________________________
📊 3️⃣ Performance Management
________________________________________
🔹 Performance_Criteria
Field	Type	Example
Criteria_ID	INT	1
Criteria_Name	VARCHAR	Productivity
Weight	INT	40
________________________________________
🔹 Employee_Performance
Field	Type	Example
Employee_ID	INT	1001
Period	VARCHAR	2026
Score	INT	85
Rating	VARCHAR	A
________________________________________
🔹 Promotion_Record
Field	Type	Example
Employee_ID	INT	1001
Old_Designation	VARCHAR	Operator
New_Designation	VARCHAR	Supervisor
Date	DATE	01-01-2026
________________________________________
Screens
•	Performance evaluation 
•	Promotion approval 
________________________________________
📄 4️⃣ Document Management
________________________________________
🔹 Document_Type
Field	Type	Example
Type_ID	INT	1
Type_Name	VARCHAR	NID
________________________________________
🔹 Employee_Document
Field	Type	Example
Employee_ID	INT	1001
Type_ID	INT	1
File_Path	VARCHAR	/docs/nid.pdf
________________________________________
🔹 Letter_Generation
Field	Type	Example
Letter_ID	INT	1
Employee_ID	INT	1001
Letter_Type	VARCHAR	Appointment
Date	DATE	01-01-2024
________________________________________
Screens
•	Upload documents 
•	Generate letters 
________________________________________
📊 5️⃣ Dashboard & Analytics
________________________________________
🔹 Dashboard_Snapshot (Optional)
Field	Type	Example
Snapshot_ID	INT	1
Date	DATE	01-03-2026
Total_Employees	INT	4200
Present	INT	3980
Absent	INT	220
*/
public class Class1
{

}
