
-- Seed data derived from NZ.HRM.Domain/Class13.cs
-- Inserts companies, departments, sections, banks, employees (basic) and employee payroll entries.
-- IDs are deterministic 26-char strings: prefixes used: CO (company), DE (department), SE (section), BA (bank), EM (employee), PR (payroll)

-- Unit (NZDY Flax Spinning Ltd) - use mst_unit as company/unit table
INSERT INTO "master"."mst_unit" ("Id", "GroupId", "UnitCode", "UnitName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "MstGroupComplexId")
SELECT 'UN000000000000000000000004', 'GR000000000000000000000001', 'nzdy_flax_spinning_ltd', 'NZDY Flax Spinning Ltd', now(), 'seed', now(), 'seed', true, 1000, 'GC000000000000000000000001'
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_unit" WHERE "UnitName" = 'NZDY Flax Spinning Ltd');

-- Banks
INSERT INTO "lookup"."bank" ("Id", "BankCode", "BankName", "RoutingNo", "ActiveFlag", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive","SortOrder")
SELECT 'BA000000000000000000000001','dutch_bangla','Dutch Bangla Bank PLC', NULL, true, now(), 'seed', now(), 'seed', true, 0
WHERE NOT EXISTS (SELECT 1 FROM "lookup"."bank" WHERE "BankName" = 'Dutch Bangla Bank PLC');

-- Departments (unique)
INSERT INTO "master"."mst_department" ("Id", "SubunitId", "DepartmentCode", "DepartmentName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'DE000000000000000000000001', 'SU000000000000000000000008', 'accounts_finance', 'Accounts & Finance', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" WHERE "DepartmentName" = 'Accounts & Finance');

INSERT INTO "master"."mst_department" ("Id", "SubunitId", "DepartmentCode", "DepartmentName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'DE000000000000000000000002', 'SU000000000000000000000008', 'commercial', 'Commercial', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" WHERE "DepartmentName" = 'Commercial');

INSERT INTO "master"."mst_department" ("Id", "SubunitId", "DepartmentCode", "DepartmentName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'DE000000000000000000000003', 'SU000000000000000000000008', 'marketing', 'Marketing', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" WHERE "DepartmentName" = 'Marketing');

INSERT INTO "master"."mst_department" ("Id", "SubunitId", "DepartmentCode", "DepartmentName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'DE000000000000000000000004', 'SU000000000000000000000008', 'incentive', 'Incentive', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" WHERE "DepartmentName" = 'Incentive');

INSERT INTO "master"."mst_department" ("Id", "SubunitId", "DepartmentCode", "DepartmentName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'DE000000000000000000000005', 'SU000000000000000000000008', 'admin_hr_compliance', 'Admin, HR & Compliance', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" WHERE "DepartmentName" = 'Admin, HR & Compliance');

-- Sections (unique) "DepartmentId",'DE000000000000000000000001',
INSERT INTO "master"."mst_section" ("Id",  "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000001',  'accounts', 'Accounts', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Accounts');
--'DE000000000000000000000002',
INSERT INTO "master"."mst_section" ("Id",  "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000002',  'commercial_textile', 'Commercial (Textile)', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Commercial (Textile)');
--'DE000000000000000000000003',
INSERT INTO "master"."mst_section" ("Id",  "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000003',  'marketing_mostak', 'Marketing (Mr. Mostak)', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Marketing (Mr. Mostak)');
--'DE000000000000000000000002',
INSERT INTO "master"."mst_section" ("Id", "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000004',  'import', 'Import', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Import');
--'DE000000000000000000000002',
INSERT INTO "master"."mst_section" ("Id", "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000005',  'commercial_fabrics', 'Commercial (Fabrics)', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Commercial (Fabrics)');
--'DE000000000000000000000004',
INSERT INTO "master"."mst_section" ("Id",  "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000006',  'overall', 'Overall', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Overall');
--'DE000000000000000000000003',
INSERT INTO "master"."mst_section" ("Id", "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000007',  'marketing_motaleb', 'Marketing (Mr. Motaleb)', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'Marketing (Mr. Motaleb)');
--'DE000000000000000000000005',
INSERT INTO "master"."mst_section" ("Id", "SectionCode", "SectionName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'SE000000000000000000000008',  'ceo_sectt', 'CEO Sectt', now(), 'seed', now(), 'seed', true, 1000
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" WHERE "SectionName" = 'CEO Sectt');

-- Employees and payroll
-- Helper: generate employee IDs EM + 24-digit zero-padded employee number

-- Row 1: 20007
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000020007', '20007', 'EM000000000000000000020007', 'NULL',NULL, 'Mohammad Tarikul Islam','Mohammad Tarikul Islam','Mohammad Tarikul Islam', 'PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '20007');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000020007', em."Id", 156000.00, 95937.50, 47968.75, 9593.75, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011010206558', NULL, 10981.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '20007' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 2: 30036
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000030036', '30036','EM000000000000000000030036', 'Card1' ,NULL, 'Kazi Amamul Haq','Kazi Amamul Haq','Kazi Amamul Haq','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '30036');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000030036', em."Id", 104000.00, 63437.50, 31718.75, 6343.75, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030595982', NULL, 2990.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '30036' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");
---st
-- Row 3: 40017
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000040017', '40017','EM000000000000000000040017', 'Card1' ,NULL, 'Bengir Hossain','Bengir Hossain','Bengir Hossain','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '40017');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000040017', em."Id", 77850.00, 47093.75, 23546.88, 4709.38, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030595753', NULL, 770.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '40017' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 4: 40030
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000040030', '40030','EM000000000000000000040017', 'Card1' ,NULL, 'Shaikh Mohiuddin Abdul kader','Shaikh Mohiuddin Abdul kader','Shaikh Mohiuddin Abdul kader','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '40030');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000040030', em."Id", 80250.00, 48593.75, 24296.88, 4859.38, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030595998', NULL, 946.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '40030' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 5: 40108
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000040108', '40108','EM000000000000000000040017', 'Card1' ,NULL, 'Abu Ahmad Samun Ahsan','Abu Ahmad Samun Ahsan','Abu Ahmad Samun Ahsan','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '40108');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000040108', em."Id", 57000.00, 34062.50, 17031.25, 3406.25, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1051010445263', NULL, 420.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '40108' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 6: 50169
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000050169', '50169','EM000000000000000000040017', 'Card1' ,NULL, 'Baydul Islam','Baydul Islam','Baydul Islam','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '50169');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000050169', em."Id", 35500.00, 20625.00, 10312.50, 2062.50, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030907899', NULL, 0.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '50169' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 7: 40074
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000040074', '40074','EM000000000000000000040017', 'Card1' ,NULL, 'Mohammad Firoze Alam','Mohammad Firoze Alam','Mohammad Firoze Alam','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '40074');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000040074', em."Id", 43850.00, 25843.75, 12921.88, 2584.38, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030596035', NULL, 420.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '40074' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");
------
-- Row 8: 50160
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000050160', '50160','EM000000000000000000040017', 'Card1' ,NULL, 'Md. Kamrul Hasan Akond','Md. Kamrul Hasan Akond','Md. Kamrul Hasan Akond','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '50160');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000050160', em."Id", 39000.00, 22812.50, 11406.25, 2281.25, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1271510004972', NULL, 0.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '50160' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 9: 50012
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000050012', '50012','EM000000000000000000040017', 'Card1' ,NULL, 'Md. Mukul Hossain','Md. Mukul Hossain','Md. Mukul Hossain','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '50012');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000050012', em."Id", 33275.00, 19234.38, 9617.19, 1923.44, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011030598734', NULL, 0.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '50012' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 10: 50369
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000050369', '50369','EM000000000000000000040017', 'Card1' ,NULL, 'Jannatun Nayem','Jannatun Nayem','Jannatun Nayem','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '50369');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000050369', em."Id", 24500.00, 13750.00, 6875.00, 1375.00, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011031434971', NULL, 0.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '50369' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- Row 11: 70358
INSERT INTO "hrm"."employee_master" ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder", "EmployeeType")
SELECT 'EM000000000000000000070358', '70358','EM000000000000000000040017', 'Card1' ,NULL, 'Md. Al Amin','Md. Al Amin','Md. Al Amin','PPG0000000001', now(), 'seed', now(), 'seed', true, 1000, 'staff'
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" WHERE "EmployeeCode" = '70358');

INSERT INTO "hrm"."employee_payroll" ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
SELECT 'PR000000000000000000070358', em."Id", 31000.00, 17812.50, 8906.25, 1781.25, 2500.00, 0.00, 0.00, 'Bank', ba."Id", '1011031543228', NULL, 0.00, now(), 'seed', now(), 'seed', true, 1000
FROM "hrm"."employee_master" em CROSS JOIN "lookup"."bank" ba
WHERE em."EmployeeCode" = '70358' AND ba."BankName" = 'Dutch Bangla Bank PLC'
  AND NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" WHERE "EmployeeId" = em."Id");

-- End of seed
