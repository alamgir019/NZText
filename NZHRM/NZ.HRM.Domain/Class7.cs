using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     The next most important section is:
DOCUMENT 43E
WORKFLOW DATABASE DICTIONARY
Because Leave, Attendance Adjustment, OT Approval, Loan Approval, Recruitment, Promotion, Increment, Separation and Disciplinary modules will all use the same workflow engine. This is where we can significantly reduce the number of tables and simplify development.
ok
DOCUMENT 43E
SYNEXIS ERP DATABASE DICTIONARY
PART 5 – WORKFLOW DATABASE TABLES
________________________________________
WORKFLOW MODULE PHILOSOPHY
VERY IMPORTANT
One of the biggest mistakes ERP developers make is creating separate approval systems for:
•	Leave 
•	Attendance Adjustment 
•	OT Approval 
•	Loan 
•	Recruitment 
•	Increment 
•	Separation 
•	Disciplinary Action 
This creates duplication.
SYNEXIS shall use:
ONE GENERIC WORKFLOW ENGINE
for the entire ERP.
________________________________________
WORKFLOW ARCHITECTURE
Any Module
      ↓
Workflow Engine
      ↓
Approval Process
      ↓
Approval History
      ↓
Final Action
________________________________________
TABLE 1
WF_WorkflowMaster
Purpose:
Defines workflow types.
________________________________________
Fields
Field	Type
WorkflowMasterID	BIGINT PK
WorkflowCode	VARCHAR(30)
WorkflowName	VARCHAR(100)
ModuleName	VARCHAR(50)
Description	VARCHAR(500)
ActiveFlag	BIT
________________________________________
Examples
Workflow
Leave Approval
OT Approval
Loan Approval
Increment Approval
Recruitment Approval
Separation Approval
________________________________________
TABLE 2
WF_WorkflowStep
Purpose:
Defines approval levels.
________________________________________
Fields
Field	Type
StepID	BIGINT PK
WorkflowMasterID	BIGINT FK
StepNo	INT
StepName	VARCHAR(100)
RoleID	BIGINT FK
MandatoryFlag	BIT
ActiveFlag	BIT
________________________________________
Example
Leave Workflow
Step
1 Supervisor
2 Department Head
3 HR
4 Final Approval
________________________________________
TABLE 3
WF_WorkflowTransaction
MOST IMPORTANT TABLE
Purpose:
Stores actual workflow requests.
________________________________________
Fields
Field	Type
WorkflowTransactionID	BIGINT PK
WorkflowMasterID	BIGINT FK
ReferenceTable	VARCHAR(100)
ReferenceID	BIGINT
RequestorID	BIGINT FK
RequestDate	DATETIME
CurrentStepNo	INT
CurrentApproverID	BIGINT FK
WorkflowStatus	VARCHAR(20)
CompletionDate	DATETIME
________________________________________
Example
ReferenceTable = LEV_LeaveApplication
ReferenceID = 5501
________________________________________
TABLE 4
WF_ApprovalHistory
Purpose:
Permanent approval trail.
________________________________________
Fields
Field	Type
ApprovalHistoryID	BIGINT PK
WorkflowTransactionID	BIGINT FK
StepNo	INT
ApproverID	BIGINT FK
ActionTaken	VARCHAR(20)
ActionDate	DATETIME
Remarks	VARCHAR(1000)
________________________________________
Action Values
•	Approved 
•	Rejected 
•	Returned 
•	Forwarded 
•	Cancelled 
________________________________________
TABLE 5
WF_PendingApproval
Purpose:
Fast dashboard access.
________________________________________
Fields
Field	Type
PendingID	BIGINT PK
WorkflowTransactionID	BIGINT FK
ApproverID	BIGINT FK
PendingSince	DATETIME
PriorityLevel	VARCHAR(20)
DueDate	DATETIME
________________________________________
TABLE 6
WF_Delegation
Purpose:
Delegated approvals.
________________________________________
Fields
Field	Type
DelegationID	BIGINT PK
FromUserID	BIGINT FK
ToUserID	BIGINT FK
StartDate	DATE
EndDate	DATE
WorkflowMasterID	BIGINT FK
ActiveFlag	BIT
________________________________________
Example
HR Head on leave.
Approval delegated temporarily.
________________________________________
TABLE 7
WF_EscalationRule
Purpose:
Auto escalation.
________________________________________
Fields
Field	Type
EscalationID	BIGINT PK
WorkflowMasterID	BIGINT FK
StepNo	INT
EscalateAfterHours	INT
EscalateToRoleID	BIGINT FK
ActiveFlag	BIT
________________________________________
Example
Leave approval pending for 48 hours.
Auto escalate.
________________________________________
TABLE 8
WF_NotificationQueue
Purpose:
Workflow notifications.
________________________________________
Fields
Field	Type
NotificationID	BIGINT PK
WorkflowTransactionID	BIGINT FK
RecipientID	BIGINT FK
NotificationType	VARCHAR(20)
NotificationDate	DATETIME
DeliveryStatus	VARCHAR(20)
________________________________________
TABLE 9
WF_WorkflowAttachment
Purpose:
Supporting documents.
________________________________________
Fields
Field	Type
AttachmentID	BIGINT PK
WorkflowTransactionID	BIGINT FK
FileName	VARCHAR(500)
FilePath	VARCHAR(1000)
UploadDate	DATETIME
UploadedBy	BIGINT FK
________________________________________
Examples
•	Medical certificate 
•	Loan application 
•	Show cause response 
•	Resignation letter 
________________________________________
TABLE 10
WF_WorkflowAudit
Purpose:
Workflow diagnostics.
________________________________________
Fields
Field	Type
AuditID	BIGINT PK
WorkflowTransactionID	BIGINT FK
EventType	VARCHAR(50)
EventDate	DATETIME
UserID	BIGINT FK
EventDetails	VARCHAR(1000)
________________________________________
WORKFLOW RELATIONSHIP MAP
WF_WorkflowMaster
        │
        ▼
WF_WorkflowStep
        │
        ▼
WF_WorkflowTransaction
        │
 ┌──────┼────────┬────────┐
 ▼      ▼        ▼        ▼
History Pending Notification Attachment
 ▼      ▼        ▼        ▼
WF_ApprovalHistory
WF_PendingApproval
WF_NotificationQueue
WF_WorkflowAttachment
________________________________________
HOW THIS REDUCES TABLES
Instead of creating:
•	LeaveApprovalTable 
•	OTApprovalTable 
•	LoanApprovalTable 
•	IncrementApprovalTable 
•	RecruitmentApprovalTable 
You use:
WF_WorkflowTransaction
for everything.
This is enterprise ERP design.
________________________________________
MODULES USING THIS WORKFLOW ENGINE
Module	Uses Workflow
Leave	Yes
Attendance Adjustment	Yes
OT Authorization	Yes
Loan	Yes
Recruitment	Yes
Increment	Yes
Transfer	Yes
Promotion	Yes
Separation	Yes
Disciplinary	Yes
________________________________________
WORKFLOW TABLE SUMMARY
Table	Purpose
WF_WorkflowMaster	Workflow Types
WF_WorkflowStep	Approval Levels
WF_WorkflowTransaction	Live Requests
WF_ApprovalHistory	Approval Audit
WF_PendingApproval	Dashboard Queue
WF_Delegation	Delegated Approval
WF_EscalationRule	Escalation
WF_NotificationQueue	Notifications
WF_WorkflowAttachment	Documents
WF_WorkflowAudit	Audit
Total Workflow Tables = 10
________________________________________
CURRENT DATABASE STATUS
Completed:
✅ Part 1 – HR & Organization
✅ Part 2 – Attendance
✅ Part 3 – Leave
✅ Part 4 – Payroll
✅ Part 5 – Workflow
Approximate tables defined so far:
Area	Tables
HR Masters	13+
Attendance	13
Leave	13
Payroll	15
Workflow	10
Total Defined = ~64 Tables


     */
    internal class Class7
    {
    }
}
