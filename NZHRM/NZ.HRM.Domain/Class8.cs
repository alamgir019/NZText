using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     DOCUMENT 43F
SYNEXIS ERP DATABASE DICTIONARY
PART 6 – SECURITY & AUDIT DATABASE TABLES
________________________________________
SECURITY PHILOSOPHY
VERY IMPORTANT
SYNEXIS shall follow:
RBAC (Role Based Access Control)
Meaning:
User
  ↓
Role
  ↓
Permission
  ↓
Module Access
Never assign permissions directly to users unless absolutely necessary.
________________________________________
SECURITY ARCHITECTURE
User
  ↓
Role
  ↓
Permission
  ↓
Screen Access
  ↓
Button Access
  ↓
Field Access
________________________________________
TABLE 1
SEC_User
Purpose:
ERP login users.
________________________________________
Fields
Field	Type
UserID	BIGINT PK
EmployeeID	BIGINT FK
UserName	VARCHAR(100)
LoginID	VARCHAR(50)
PasswordHash	VARCHAR(500)
Email	VARCHAR(100)
MobileNo	VARCHAR(20)
LastLoginDate	DATETIME
ActiveFlag	BIT
CreatedDate	DATETIME
________________________________________
TABLE 2
SEC_Role
Purpose:
Role definitions.
________________________________________
Fields
Field	Type
RoleID	BIGINT PK
RoleCode	VARCHAR(30)
RoleName	VARCHAR(100)
Description	VARCHAR(500)
ActiveFlag	BIT
________________________________________
Examples
•	HR Officer 
•	HR Manager 
•	Payroll Officer 
•	Payroll Manager 
•	Attendance Officer 
•	Unit Head 
•	Group HR Head 
•	System Administrator 
________________________________________
TABLE 3
SEC_Permission
Purpose:
Permission catalog.
________________________________________
Fields
Field	Type
PermissionID	BIGINT PK
PermissionCode	VARCHAR(50)
PermissionName	VARCHAR(100)
ModuleName	VARCHAR(50)
PermissionType	VARCHAR(20)
ActiveFlag	BIT
________________________________________
Permission Types
•	View 
•	Add 
•	Edit 
•	Delete 
•	Approve 
•	Export 
•	Lock 
•	Unlock 
________________________________________
TABLE 4
SEC_UserRole
Purpose:
User-role assignment.
________________________________________
Fields
Field	Type
UserRoleID	BIGINT PK
UserID	BIGINT FK
RoleID	BIGINT FK
EffectiveDate	DATE
ExpiryDate	DATE
ActiveFlag	BIT
________________________________________
TABLE 5
SEC_RolePermission
Purpose:
Role permission assignment.
________________________________________
Fields
Field	Type
RolePermissionID	BIGINT PK
RoleID	BIGINT FK
PermissionID	BIGINT FK
ActiveFlag	BIT
________________________________________
TABLE 6
SEC_ModuleAccess
Purpose:
Module-level access.
________________________________________
Fields
Field	Type
ModuleAccessID	BIGINT PK
RoleID	BIGINT FK
ModuleCode	VARCHAR(30)
CanView	BIT
CanAdd	BIT
CanEdit	BIT
CanDelete	BIT
CanApprove	BIT
CanExport	BIT
________________________________________
Example
Payroll Module
Role	Access
Payroll Manager	Full
HR Officer	View Only
Production Manager	No Access
________________________________________
TABLE 7
SEC_FieldSecurity
Purpose:
Field-level restrictions.
VERY IMPORTANT.
________________________________________
Fields
Field	Type
FieldSecurityID	BIGINT PK
RoleID	BIGINT FK
ScreenCode	VARCHAR(30)
FieldName	VARCHAR(100)
CanView	BIT
CanEdit	BIT
________________________________________
Examples
Sensitive Fields:
•	GrossSalary 
•	BankAccountNo 
•	TINNo 
•	NID 
________________________________________
TABLE 8
SEC_UserSession
Purpose:
Active session management.
________________________________________
Fields
Field	Type
SessionID	BIGINT PK
UserID	BIGINT FK
LoginDateTime	DATETIME
LogoutDateTime	DATETIME
IPAddress	VARCHAR(50)
DeviceInfo	VARCHAR(500)
SessionStatus	VARCHAR(20)
________________________________________
Status
•	Active 
•	Expired 
•	Logged Out 
________________________________________
TABLE 9
SEC_PasswordHistory
Purpose:
Password policy enforcement.
________________________________________
Fields
Field	Type
PasswordHistoryID	BIGINT PK
UserID	BIGINT FK
PasswordHash	VARCHAR(500)
ChangedDate	DATETIME
________________________________________
TABLE 10
SEC_EmergencyAccess
Purpose:
Temporary elevated access.
________________________________________
Fields
Field	Type
EmergencyAccessID	BIGINT PK
UserID	BIGINT FK
GrantedBy	BIGINT FK
StartDateTime	DATETIME
EndDateTime	DATETIME
Reason	VARCHAR(1000)
Status	VARCHAR(20)
________________________________________
AUDIT PHILOSOPHY
VERY IMPORTANT
ERP must answer:
•	Who? 
•	What? 
•	When? 
•	Old Value? 
•	New Value? 
for every important change.
________________________________________
TABLE 11
AUD_LoginHistory
Purpose:
Login audit.
________________________________________
Fields
Field	Type
LoginHistoryID	BIGINT PK
UserID	BIGINT FK
LoginDateTime	DATETIME
LogoutDateTime	DATETIME
IPAddress	VARCHAR(50)
LoginStatus	VARCHAR(20)
________________________________________
TABLE 12
AUD_DataChange
MOST IMPORTANT AUDIT TABLE
________________________________________
Fields
Field	Type
AuditID	BIGINT PK
TableName	VARCHAR(100)
RecordID	BIGINT
FieldName	VARCHAR(100)
OldValue	VARCHAR(MAX)
NewValue	VARCHAR(MAX)
ChangedBy	BIGINT FK
ChangeDate	DATETIME
________________________________________
TABLE 13
AUD_ApprovalTrail
Purpose:
Tracks approvals.
________________________________________
Fields
Field	Type
ApprovalTrailID	BIGINT PK
WorkflowTransactionID	BIGINT FK
ApproverID	BIGINT FK
ActionTaken	VARCHAR(20)
ActionDate	DATETIME
Remarks	VARCHAR(1000)
________________________________________
TABLE 14
AUD_ReportAccess
Purpose:
Tracks report viewing.
________________________________________
Fields
Field	Type
ReportAccessID	BIGINT PK
UserID	BIGINT FK
ReportName	VARCHAR(100)
AccessDateTime	DATETIME
ExportFlag	BIT
________________________________________
TABLE 15
AUD_ExportHistory
VERY IMPORTANT
Purpose:
Tracks exported data.
________________________________________
Fields
Field	Type
ExportHistoryID	BIGINT PK
UserID	BIGINT FK
ModuleName	VARCHAR(50)
ExportType	VARCHAR(20)
ExportDateTime	DATETIME
RecordCount	INT
________________________________________
TABLE 16
AUD_SystemEvent
Purpose:
Tracks system events.
________________________________________
Fields
Field	Type
EventID	BIGINT PK
EventType	VARCHAR(50)
EventDateTime	DATETIME
UserID	BIGINT FK
EventDescription	VARCHAR(1000)
________________________________________
Examples
•	Payroll Locked 
•	Attendance Reprocessed 
•	OT Approval Modified 
•	Workflow Escalated 
•	Security Violation 
________________________________________
SECURITY & AUDIT RELATIONSHIP MAP
SEC_User
    │
    ▼
SEC_UserRole
    │
    ▼
SEC_Role
    │
    ▼
SEC_RolePermission
    │
    ▼
SEC_Permission

SEC_User
    │
 ┌──┼──────────┬──────────┐
 ▼  ▼          ▼          ▼
Login Audit   Export    Reports
History       History   Access
________________________________________
SECURITY & AUDIT TABLE SUMMARY
Table	Purpose
SEC_User	Users
SEC_Role	Roles
SEC_Permission	Permissions
SEC_UserRole	User-Role Mapping
SEC_RolePermission	Role-Permission Mapping
SEC_ModuleAccess	Module Access
SEC_FieldSecurity	Field Access
SEC_UserSession	Session Management
SEC_PasswordHistory	Password Audit
SEC_EmergencyAccess	Temporary Access
AUD_LoginHistory	Login Audit
AUD_DataChange	Data Changes
AUD_ApprovalTrail	Approval Audit
AUD_ReportAccess	Report Audit
AUD_ExportHistory	Export Audit
AUD_SystemEvent	System Events
Total Security & Audit Tables = 16


     */
    internal class Class8
    {
    }
}
