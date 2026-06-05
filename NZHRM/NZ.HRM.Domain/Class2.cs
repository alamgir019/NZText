using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{


    /*
SYNEXIS ERP POSTGRESQL DATABASE CONSTITUTION VERSION 1.0
DATABASE NAME: synexis_erp
SCHEMA CREATION ORDER
01 master
02 lookup
03 hrm
04 attendance
05 leave_mgmt
06 workflow
07 payroll
08 security
09 audit
SCHEMA 01 : master
Tables:
mst_group
mst_unit
mst_subunit
mst_department
mst_section
mst_cell
mst_shift
mst_grade
mst_designation
mst_employee_category
mst_leave_type
mst_holiday_calendar
mst_company_calendar
SCHEMA 02 : lookup
Tables:
gender
religion
marital_status
blood_group
education_level
document_type
bank
country
SCHEMA 03 : hrm
Tables:
employee_master
employee_documents
employee_nominee
employee_education
employee_experience
employee_training
employee_family
employee_bank_account
employee_reporting

Key Foreign Keys:
employee_master.group_id -> master.mst_group.group_id
employee_master.unit_id -> master.mst_unit.unit_id
employee_master.subunit_id -> master.mst_subunit.subunit_id
employee_master.department_id -> master.mst_department.department_id
employee_master.section_id -> master.mst_section.section_id
employee_master.cell_id -> master.mst_cell.cell_id
employee_master.shift_id -> master.mst_shift.shift_id
employee_master.grade_id -> master.mst_grade.grade_id
employee_master.designation_id -> master.mst_designation.designation_id
employee_master.employee_category_id -> master.mst_employee_category.employee_category_id
SCHEMA 04 : attendance
Tables:
device_master
device_sync_log
raw_punch
shift_roster
ot_authorization
processed_attendance
attendance_exception
attendance_adjustment
attendance_lock
processing_log
inside_factory_status
weekly_off_pattern
SCHEMA 05 : leave_mgmt
Tables:
leave_balance
leave_application
leave_application_details
leave_adjustment
leave_opening_balance
leave_encashment
leave_accrual
leave_approval_history
leave_cancellation
leave_year
leave_policy
SCHEMA 06 : workflow
Tables:
workflow_master
workflow_step
workflow_transaction
approval_history
pending_approval
delegation
escalation_rule
notification_queue
workflow_attachment
workflow_audit
SCHEMA 07 : payroll
Tables:
salary_structure
salary_component
increment_history
payroll_header
payroll_details
ot_details
deduction
arrear
bonus
tax
loan_recovery
bank_transfer
payslip
payroll_adjustment
payroll_lock
payroll_process_log
SCHEMA 08 : security
Tables:
user_account
role
permission
user_role
role_permission
module_access
field_security
user_session
password_history
emergency_access
SCHEMA 09 : audit
Tables:
login_history
data_change
approval_trail
report_access
export_history
system_event
OFFICIAL RULES
All schema names: lowercase only
All table names: lowercase only
Words separated by underscore
Primary key naming: table_name_id
Tracking fields: created_at, updated_at, created_by, updated_by
No new schemas, table renaming, duplicate workflow/payroll/attendance tables without ERP Owner approval.

*/
    class Class2
    {
    }
}
