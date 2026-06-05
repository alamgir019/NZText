using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZ.HRM.Domain
{
    /*
     DOCUMENT 43A-1
LOOKUP SCHEMA DATABASE DICTIONARY
between:
43A HR & Organization
43A-1 Lookup
43B Attendance
43C Leave
43D Payroll
43E Workflow
43F Security & Audit
________________________________________
PURPOSE OF LOOKUP SCHEMA
The lookup schema contains small reference tables used throughout the ERP.
Benefits:
•	No hardcoding 
•	Easier maintenance 
•	Dropdown management 
•	Multi-language support in future 
•	Better reporting 
________________________________________
SCHEMA
lookup
________________________________________
TABLE 1
lookup.gender
Field Name	Type
gender_id	BIGSERIAL PK
gender_code	VARCHAR(10)
gender_name	VARCHAR(30)
display_order	INTEGER
active_flag	BOOLEAN
Examples:
M   Male
F   Female
O   Other
________________________________________
TABLE 2
lookup.religion
Field Name	Type
religion_id	BIGSERIAL PK
religion_code	VARCHAR(10)
religion_name	VARCHAR(50)
display_order	INTEGER
active_flag	BOOLEAN
Examples:
Islam
Hinduism
Buddhism
Christianity
________________________________________
TABLE 3
lookup.marital_status
Field Name	Type
marital_status_id	BIGSERIAL PK
status_code	VARCHAR(10)
status_name	VARCHAR(30)
active_flag	BOOLEAN
Examples:
Single
Married
Divorced
Widowed
________________________________________
TABLE 4
lookup.blood_group
Field Name	Type
blood_group_id	BIGSERIAL PK
blood_group_code	VARCHAR(10)
blood_group_name	VARCHAR(10)
active_flag	BOOLEAN
Examples:
A+
A-
B+
B-
AB+
AB-
O+
O-
________________________________________
TABLE 5
lookup.education_level
Field Name	Type
education_level_id	BIGSERIAL PK
education_code	VARCHAR(20)
education_name	VARCHAR(100)
display_order	INTEGER
active_flag	BOOLEAN
Examples:
SSC
HSC
Diploma
Bachelor
Masters
PhD
________________________________________
TABLE 6
lookup.document_type
Field Name	Type
document_type_id	BIGSERIAL PK
document_code	VARCHAR(20)
document_name	VARCHAR(100)
mandatory_flag	BOOLEAN
active_flag	BOOLEAN
Examples:
NID
Passport
Birth Certificate
Photo
Certificate
Appointment Letter
________________________________________
TABLE 7
lookup.bank
Field Name	Type
bank_id	BIGSERIAL PK
bank_code	VARCHAR(20)
bank_name	VARCHAR(100)
routing_no	VARCHAR(30)
active_flag	BOOLEAN
Examples:
Dutch Bangla Bank
BRAC Bank
City Bank
Islami Bank
________________________________________
TABLE 8
lookup.country
Field Name	Type
country_id	BIGSERIAL PK
country_code	VARCHAR(5)
country_name	VARCHAR(100)
nationality_name	VARCHAR(100)
active_flag	BOOLEAN
Examples:
Bangladesh
India
Pakistan
Sri Lanka
Canada
Australia
________________________________________
FOREIGN KEY USAGE
These tables will be referenced by:
hrm.employee_master
Examples:
Employee Master Field	Lookup Table
gender_id	lookup.gender
religion_id	lookup.religion
marital_status_id	lookup.marital_status
blood_group_id	lookup.blood_group
nationality_id	lookup.country
bank_id	lookup.bank
________________________________________
UPDATED DATABASE SUMMARY
Schema	Tables
master	13
lookup	8
hrm	9
attendance	13
leave_mgmt	11
workflow	10
payroll	16
security	10
audit	6
Current Total = 96 Tables


     */
    internal class Class9
    {
    }
}
