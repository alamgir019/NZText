using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     hrm.employee_master
Field Name	Type
employee_id	BIGSERIAL PK
employee_code	VARCHAR(20) UNIQUE
card_no	VARCHAR(20)
old_card_no	VARCHAR(20)
employee_name	VARCHAR(150)
employee_name_bangla	VARCHAR(150)
father_name	VARCHAR(150)
mother_name	VARCHAR(150)
spouse_name	VARCHAR(150)
gender_id	BIGINT FK
religion_id	BIGINT FK
marital_status_id	BIGINT FK
blood_group_id	BIGINT FK
nationality_id	BIGINT FK
date_of_birth	DATE
nid_no	VARCHAR(30)
birth_certificate_no	VARCHAR(30)
passport_no	VARCHAR(30)
mobile_no	VARCHAR(20)
emergency_contact_no	VARCHAR(20)
personal_email	VARCHAR(100)
present_address	TEXT
permanent_address	TEXT
joining_date	DATE
confirmation_date	DATE
resignation_date	DATE
separation_date	DATE
group_id	BIGINT FK
unit_id	BIGINT FK
subunit_id	BIGINT FK
department_id	BIGINT FK
section_id	BIGINT FK
cell_id	BIGINT FK
designation_id	BIGINT FK
grade_id	BIGINT FK
shift_id	BIGINT FK
employee_category_id	BIGINT FK
reporting_employee_id	BIGINT FK
gross_salary	NUMERIC(12,2)
bank_id	BIGINT FK
bank_account_no	VARCHAR(50)
active_flag	BOOLEAN
created_at	TIMESTAMP
created_by	BIGINT
updated_at	TIMESTAMP
updated_by	BIGINT
________________________________________
hrm.employee_documents
Field Name	Type
document_id	BIGSERIAL PK
employee_id	BIGINT FK
document_type_id	BIGINT FK
document_no	VARCHAR(100)
issue_date	DATE
expiry_date	DATE
file_name	VARCHAR(255)
file_path	VARCHAR(1000)
remarks	VARCHAR(500)
active_flag	BOOLEAN
created_at	TIMESTAMP
created_by	BIGINT
Examples:
•	NID 
•	Passport 
•	Photo 
•	Appointment Letter 
•	Educational Certificate 
________________________________________
hrm.employee_nominee
Field Name	Type
nominee_id	BIGSERIAL PK
employee_id	BIGINT FK
nominee_name	VARCHAR(150)
relationship	VARCHAR(50)
date_of_birth	DATE
nid_no	VARCHAR(30)
mobile_no	VARCHAR(20)
address	TEXT
nomination_percentage	NUMERIC(5,2)
active_flag	BOOLEAN
created_at	TIMESTAMP
created_by	BIGINT
Business Rule:
Total nomination percentage = 100%
________________________________________
hrm.employee_education
Field Name	Type
education_id	BIGSERIAL PK
employee_id	BIGINT FK
education_level_id	BIGINT FK
institute_name	VARCHAR(200)
board_university	VARCHAR(200)
passing_year	INTEGER
result_gpa	VARCHAR(20)
major_subject	VARCHAR(100)
certificate_no	VARCHAR(100)
active_flag	BOOLEAN
created_at	TIMESTAMP
________________________________________
hrm.employee_experience
Field Name	Type
experience_id	BIGSERIAL PK
employee_id	BIGINT FK
company_name	VARCHAR(200)
designation	VARCHAR(100)
joining_date	DATE
leaving_date	DATE
last_salary	NUMERIC(12,2)
responsibilities	TEXT
reason_for_leaving	VARCHAR(500)
active_flag	BOOLEAN
created_at	TIMESTAMP
________________________________________
hrm.employee_training
Field Name	Type
training_id	BIGSERIAL PK
employee_id	BIGINT FK
training_name	VARCHAR(200)
training_provider	VARCHAR(200)
start_date	DATE
end_date	DATE
training_hours	NUMERIC(8,2)
certificate_received	BOOLEAN
certificate_no	VARCHAR(100)
remarks	VARCHAR(500)
active_flag	BOOLEAN
Examples:
•	Fire Safety 
•	Compliance 
•	Leadership 
•	Lean Manufacturing 
•	ERP Training 
________________________________________
hrm.employee_family
Field Name	Type
family_member_id	BIGSERIAL PK
employee_id	BIGINT FK
family_member_name	VARCHAR(150)
relationship	VARCHAR(50)
date_of_birth	DATE
occupation	VARCHAR(100)
mobile_no	VARCHAR(20)
dependent_flag	BOOLEAN
active_flag	BOOLEAN
created_at	TIMESTAMP
Examples:
Father
Mother
Spouse
Son
Daughter
________________________________________
hrm.employee_bank_account
Field Name	Type
employee_bank_account_id	BIGSERIAL PK
employee_id	BIGINT FK
bank_id	BIGINT FK
account_name	VARCHAR(150)
account_no	VARCHAR(50)
routing_no	VARCHAR(30)
branch_name	VARCHAR(100)
mobile_banking_flag	BOOLEAN
salary_account_flag	BOOLEAN
active_flag	BOOLEAN
created_at	TIMESTAMP
________________________________________
hrm.employee_reporting
This is a very important table.
Field Name	Type
reporting_id	BIGSERIAL PK
employee_id	BIGINT FK
reporting_employee_id	BIGINT FK
reporting_type	VARCHAR(30)
effective_from	DATE
effective_to	DATE
active_flag	BOOLEAN
created_at	TIMESTAMP
Examples:
Employee	Reports To
Operator	Supervisor
Supervisor	Manager
Manager	GM
GM	Director
Reporting Types:
FUNCTIONAL
ADMINISTRATIVE
DOTTED_LINE
HRM Schema Summary
Table	Approx Fields
employee_master	45
employee_documents	12
employee_nominee	11
employee_education	10
employee_experience	10
employee_training	11
employee_family	10
employee_bank_account	11
employee_reporting	8
Total HRM Tables = 9
Total HRM Fields ≈ 128


     */
    internal class Class10
    {
    }
}
