using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     However, if your vision is that SYNEXIS should remain useful for the next 10–15 years and eventually become a complete textile-group ERP, then I would add the following schemas now, even if some modules are activated later.
This avoids database redesign in the future.
________________________________________
SCHEMA 10
recruitment
Purpose:
Complete recruitment lifecycle.
Tables:
recruitment.manpower_requisition

recruitment.job_position

recruitment.candidate

recruitment.candidate_documents

recruitment.interview_schedule

recruitment.interview_evaluation

recruitment.interview_panel

recruitment.offer_letter

recruitment.appointment

recruitment.recruitment_workflow
Most Important Fields
recruitment.candidate
candidate_id PK
candidate_code
candidate_name
mobile_no
email
date_of_birth
gender_id
expected_salary
current_salary
source
status
created_at
________________________________________
SCHEMA 11
loan
Purpose:
Employee loans and advances.
Tables:
loan.loan_type

loan.loan_application

loan.loan_master

loan.loan_installment

loan.loan_recovery_schedule

loan.loan_adjustment

loan.loan_settlement

loan.loan_workflow
loan.loan_master
loan_id PK
employee_id FK
loan_type_id FK
loan_amount
interest_rate
installment_count
installment_amount
loan_date
status
________________________________________
SCHEMA 12
disciplinary
Purpose:
Disciplinary management.
Tables:
disciplinary.incident

disciplinary.show_cause

disciplinary.show_cause_reply

disciplinary.investigation

disciplinary.investigation_member

disciplinary.hearing

disciplinary.punishment

disciplinary.disciplinary_workflow
disciplinary.incident
incident_id PK
employee_id FK
incident_date
incident_type
incident_details
reported_by
status
________________________________________
SCHEMA 13
separation
Purpose:
Resignation and final settlement.
Tables:
separation.resignation

separation.clearance

separation.clearance_details

separation.exit_interview

separation.final_settlement

separation.gratuity

separation.service_benefit

separation.separation_workflow
separation.final_settlement
settlement_id PK
employee_id FK
last_working_date
leave_encashment
salary_payable
loan_recovery
gratuity_amount
net_payable
________________________________________
SCHEMA 14
ess
(Employee Self Service)
Purpose:
Future mobile app.
Tables:
ess.employee_request

ess.employee_ticket

ess.employee_notification

ess.employee_acknowledgement

ess.mobile_login_log

ess.employee_dashboard
________________________________________
SCHEMA 15
communication
Purpose:
SMS, Email, Push Notifications.
Tables:
communication.message_template

communication.sms_queue

communication.email_queue

communication.push_notification

communication.communication_log
________________________________________
SCHEMA 16
integration
Purpose:
External systems integration.
Tables:
integration.biometric_sync_log

integration.bank_api_log

integration.sms_api_log

integration.email_api_log

integration.erp_import_log

integration.erp_export_log
________________________________________
SCHEMA 17
reports
Purpose:
Reporting Layer.
Important:
No transaction tables here.
Only:
reports.hr_views

reports.attendance_views

reports.leave_views

reports.payroll_views

reports.dashboard_views

reports.materialized_reports
________________________________________
SCHEMA 18
production_link
This is my additional recommendation specifically for NZ Denim / NZ Textile.
Purpose:
Connect HR and Production.
Tables:
production_link.line_master

production_link.line_target

production_link.line_output

production_link.operator_assignment

production_link.skill_matrix

production_link.machine_assignment
skill_matrix
Very important.
skill_matrix_id PK
employee_id FK
operation_name
skill_level
evaluation_date
evaluator_id
This becomes extremely useful for:
•	Production planning 
•	Operator balancing 
•	Multi-skill management 
•	AI-based line optimization later 
________________________________________
FINAL SYNEXIS ERP ARCHITECTURE
Phase 1 (Build Immediately)
master
lookup
hrm
attendance
leave_mgmt
workflow
payroll
security
audit
Approx tables: 95–100
________________________________________
Phase 2 (Build Alongside Core)
loan
recruitment
separation
disciplinary
Approx tables: 35–40
________________________________________
Phase 3 (Future Ready)
ess
communication
integration
reports
production_link
Approx tables: 25–30


     */
    internal class Class11
    {
    }
}
