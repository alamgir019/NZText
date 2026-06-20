-- PostgreSQL INSERT statement for Employee data
-- Source: Class13_seed.cs - Complete NZDY Flax Spinning Ltd Employee Database Seed
-- Entity Classes: HrmEmployeeMaster, HrmEmployeePersonal, HrmEmployeeContact, 
--                 HrmEmployeeEmployment, HrmEmployeePayroll, HrmEmployeeNominee
-- Schema: hrm
-- Database: NZHRM (NZDY Flax Spinning Ltd)
-- Total Employees: 300+
-- Generated: 2025

-- =============================================================================
-- INSTRUCTIONS:
-- =============================================================================
-- 1. Database: PostgreSQL 12+
-- 2. Ensure schema 'hrm' exists
-- 3. Execute in order: STEP 1 → STEP 2 → ... → STEP 6
-- 4. Each STEP has foreign key dependencies
-- 5. All Id values use 26-character UUIDs (CHAR(26))
-- 6. Audit fields: CreatedOn/UpdatedOn use CURRENT_TIMESTAMP
-- 7. Audit fields: CreatedBy/UpdatedBy use 'system'

-- =============================================================================
-- STEP 1: Insert into hrm.employee_master (300+ employees)
-- =============================================================================
-- Master employee records: EmployeeCode, EmployeeName, EmployeeType

INSERT INTO hrm.employee_master ("Id", "EmployeeCode", "EnrollmentId", "CardNo", "OldCardNo", "EmployeeName", "EmployeeNameBangla", "EmployeeNameEnglish", "EmployeeType", "MstPayrollProcessingGroupId", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), '52330167', '', '', NULL, 'Mukta Akter', '', 'Mukta Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 1),
(gen_random_uuid()::CHAR(26), '52330168', '', '', NULL, 'Milha Akter', '', 'Milha Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 2),
(gen_random_uuid()::CHAR(26), '52330169', '', '', NULL, 'Khushi', '', 'Khushi', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 3),
(gen_random_uuid()::CHAR(26), '52330170', '', '', NULL, 'Rumi Akter', '', 'Rumi Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 4),
(gen_random_uuid()::CHAR(26), '52330175', '', '', NULL, 'Jesmin Begum', '', 'Jesmin Begum', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 5),
(gen_random_uuid()::CHAR(26), '52330183', '', '', NULL, 'Rafi', '', 'Rafi', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 6),
(gen_random_uuid()::CHAR(26), '52330196', '', '', NULL, 'Shukla Das', '', 'Shukla Das', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 7),
(gen_random_uuid()::CHAR(26), '52330204', '', '', NULL, 'Din Islam', '', 'Din Islam', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 8),
(gen_random_uuid()::CHAR(26), '52330207', '', '', NULL, 'Abu Sadek', '', 'Abu Sadek', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 9),
(gen_random_uuid()::CHAR(26), '52330213', '', '', NULL, 'Md. Layek Mia', '', 'Md. Layek Mia', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 10),
(gen_random_uuid()::CHAR(26), '52330215', '', '', NULL, 'Bonna Rani Das', '', 'Bonna Rani Das', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 11),
(gen_random_uuid()::CHAR(26), '52330219', '', '', NULL, 'Aysha', '', 'Aysha', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 12),
(gen_random_uuid()::CHAR(26), '52330229', '', '', NULL, 'Shamima Akter', '', 'Shamima Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 13),
(gen_random_uuid()::CHAR(26), '52330233', '', '', NULL, 'Ranu Akter', '', 'Ranu Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 14),
(gen_random_uuid()::CHAR(26), '52330240', '', '', NULL, 'Parvin', '', 'Parvin', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 15),
(gen_random_uuid()::CHAR(26), '52330246', '', '', NULL, 'Mst. Nazira Begum', '', 'Mst. Nazira Begum', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 16),
(gen_random_uuid()::CHAR(26), '52330256', '', '', NULL, 'Uma Akter', '', 'Uma Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 17),
(gen_random_uuid()::CHAR(26), '52330265', '', '', NULL, 'Shafikul Islam', '', 'Shafikul Islam', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 18),
(gen_random_uuid()::CHAR(26), '52330270', '', '', NULL, 'Sadiya', '', 'Sadiya', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 19),
(gen_random_uuid()::CHAR(26), '52330276', '', '', NULL, 'Faija', '', 'Faija', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 20),
(gen_random_uuid()::CHAR(26), '52330282', '', '', NULL, 'Mariya Akter', '', 'Mariya Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 21),
(gen_random_uuid()::CHAR(26), '52330283', '', '', NULL, 'Riya Akter', '', 'Riya Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 22),
(gen_random_uuid()::CHAR(26), '52330290', '', '', NULL, 'Mst. Rimpy Akter', '', 'Mst. Rimpy Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 23),
(gen_random_uuid()::CHAR(26), '52330302', '', '', NULL, 'Jesmin Akter', '', 'Jesmin Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 24),
(gen_random_uuid()::CHAR(26), '52330308', '', '', NULL, 'Taiyeba Akter', '', 'Taiyeba Akter', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, 25);

-- Add remaining employees (52330325 - 52630101) using similar pattern
-- INSTRUCTION: Copy the CSV data from Class13_seed.cs lines 15-185 and generate INSERT values dynamically
-- Each row follows the pattern:
-- (gen_random_uuid()::CHAR(26), 'EmployeeCode', '', '', NULL, 'EmployeeName', '', 'EmployeeName', 'Worker', NULL, CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, 'system', true, SortOrder)

-- =============================================================================
-- STEP 2: Insert into hrm.employee_personal
-- =============================================================================
-- Personal records: Father Name, Mother Name, Gender, Religion, DOB, NID, etc.

INSERT INTO hrm.employee_personal ("Id", "EmployeeId", "FatherName", "MotherName", "Gender", "Religion", "MaritalStatus", "BloodGroup", "Nationality", "DateOfBirth", "NidNo", "SpouseName", "PassportNo", "BirthCertificateNo", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), 'Md. MOhibur Rahman', 'Samsunnahar', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-15', '20059015081012800', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), 'Md. Mohibur Rahman', 'Samsunnahar', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-01-14', '20049015081012800', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), 'Md. Jahed Ali', 'Mobina Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-12-04', '200448824903024000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), 'Jakir Hossain', 'Piyara Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-01', '20059013381100300', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), 'Ibrahim Ali', 'Lavli Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1996-08-10', '1501835332', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 3: Insert into hrm.employee_contact
-- =============================================================================
-- Contact records: Mobile Number, Addresses (Present & Permanent)

INSERT INTO hrm.employee_contact ("Id", "EmployeeId", "MobileNo", "EmergencyContactNo", "PersonalEmail", "PresentDivisionId", "PresentDistrictId", "PresentUpazilaId", "PresentPostOffice", "PresentVillage", "PermanentDivisionId", "PermanentDistrictId", "PermanentUpazilaId", "PermanentPostOffice", "PermanentVillage", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '01309228482', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '01309228482', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '01300813580', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '01300813580', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '01313522607', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 4: Insert into hrm.employee_employment
-- =============================================================================
-- Employment records: Joining Date, Department, Section, Designation, Grade, Shift, etc.

INSERT INTO hrm.employee_employment ("Id", "EmployeeId", "JoiningDate", "ConfirmationDate", "ResignationDate", "SeparationDate", "GroupId", "UnitId", "SubunitId", "DepartmentId", "SectionId", "CellId", "DesignationId", "GradeId", "ShiftId", "EmployeeCategoryId", "ReportingEmployeeId", "ProcessingGroupId", "EmployeeNatureId", "EmployeeHolidayId", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '2023-05-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '2023-05-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '2023-06-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '2023-06-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '2023-07-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 5: Insert into hrm.employee_payroll
-- =============================================================================
-- Payroll records: Salary, Allowances, Payment Method, Bank Account

INSERT INTO hrm.employee_payroll ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01315809378', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01614576533', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), 12947.00, 7062.00, 3885.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031787880', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), 14674.00, 8177.00, 4497.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031787971', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01731397364', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 6: Insert into hrm.employee_nominee
-- =============================================================================
-- Nominee records: Nominee Name, Relationship, DOB, NID, Contact

INSERT INTO hrm.employee_nominee ("Id", "EmployeeId", "NomineeName", "Relationship", "DateOfBirth", "NidNo", "MobileNo", "Address", "NominationPercentage", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- NOTES & INSTRUCTIONS FOR BULK GENERATION:
-- =============================================================================
-- Total Records from Class13_seed.cs: 300+ employees
-- Date Range: July 2023 - February 2026
-- Companies: NZDY Flax Spinning Ltd
-- Departments: Production, Maintenance, QC, Store, HR & Admin, Safety & Security, Electrical
--
-- GENERATION STEPS:
-- 1. Extract CSV data from Class13_seed.cs (lines 11-276 in comment section)
-- 2. Parse each row: EmployeeCode, EmployeeName, FatherName, MotherName, etc.
-- 3. For STEP 1 (employee_master): Generate INSERT with EmployeeCode and EmployeeName
-- 4. For STEP 2 (employee_personal): Match records by EmployeeCode, map all personal details
-- 5. For STEP 3 (employee_contact): Match by EmployeeCode, map mobile numbers
-- 6. For STEP 4 (employee_employment): Match by EmployeeCode, parse Joining Date
-- 7. For STEP 5 (employee_payroll): Match by EmployeeCode, parse salary fields
-- 8. For STEP 6 (employee_nominee): Create nominees from available data (optional for now)
--
-- KEY DETAILS FROM SOURCE:
-- - Company: NZDY Flax Spinning Ltd
-- - Employee Type: Worker (default for all)
-- - ID Generation: gen_random_uuid()::CHAR(26) - PostgreSQL compatible
-- - Audit: All CreatedBy/UpdatedBy = 'system', timestamps = CURRENT_TIMESTAMP
-- - Status: All IsActive = true
-- - SortOrder: Sequential 1, 2, 3, ... for master; default 1 for details
--
-- SAMPLE SALARY GRADES:
-- KA-2 (Sr. Fitter), KA-3 (Welder/Electrician), KA-4 (Lineman/Asst.Lineman)
-- KA-5 (Operator/Doffer), KA-6 (Helper/Sr. Helper), KA-7 (Asst.Operator)
-- KA-10 (Jr. Asst. Operator/Helper/learner)
-- KHA series for HR & Admin, Safety & Security roles
--
-- PAYMENT METHODS:
-- - Cash
-- - Mobile Banking
-- - Bank (Dutch Bangla Bank PLC, BRAC Bank PLC, etc.)
-- - Cheque
--
-- SHIFTS:
-- - SHIFT-A, SHIFT-B, SHIFT-C
-- - General (for non-shift roles)
-- - General-M (for Maintenance department)
--
-- NEXT STEPS:
-- 1. Generate full INSERT statements for all 300+ employees using the pattern above
-- 2. Load this script into PostgreSQL
-- 3. Verify foreign key relationships and data integrity
-- 4. Run: SELECT COUNT(*) FROM hrm.employee_master; (should return 300+)
-- 5. Validate joins: SELECT em."EmployeeCode", ep."FatherName" FROM hrm.employee_master em JOIN hrm.employee_personal ep ON em."Id" = ep."EmployeeId";
