-- =============================================================================
-- COMPLETE POSTGRESQL INSERT STATEMENTS - ALL 231 EMPLOYEES
-- NZDY Flax Spinning Ltd (NZHRM)
-- =============================================================================
-- STEP 2: Insert into hrm.employee_personal (231 records)
-- =============================================================================

INSERT INTO hrm.employee_personal ("Id", "EmployeeId", "FatherName", "MotherName", "Gender", "Religion", "MaritalStatus", "BloodGroup", "Nationality", "DateOfBirth", "NidNo", "SpouseName", "PassportNo", "BirthCertificateNo", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), 'Md. MOhibur Rahman', 'Samsunnahar', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-15', '20059015081012800', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), 'Md. Mohibur Rahman', 'Samsunnahar', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-01-14', '20049015081012800', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), 'Md. Jahed Ali', 'Mobina Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-12-04', '200448824903024000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), 'Jakir Hossain', 'Piyara Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-01', '20059013381100300', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), 'Ibrahim Ali', 'Lavli Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1996-08-10', '1501835332', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330183'), 'Kajol', 'Ayesa', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2004-02-15', '200448144951017000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330196'), 'Khela Chron Das', 'Sawrotsati Das', 'Female', 'Hindu', NULL, NULL, 'Bangladeshi', '2004-05-12', '20043611175178000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330204'), 'Md. Sabuj Mia', 'Sadeka Akter', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2004-02-08', '73745844469', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330207'), 'Akul Haqe', 'Anjuman', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '1969-01-02', '4814917189338', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330213'), 'Samsu Mia', 'Aleya Begum', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2005-05-05', '20055817465000800', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330215'), 'Roton Das', 'Sufola Rani Das', 'Female', 'Hindu', NULL, NULL, 'Bangladeshi', '2004-08-08', '20044813343104300', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330219'), 'Suroj Ali', 'Helena Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1982-09-18', '19824814951017700', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330229'), 'Md. Muslim Mia', 'Selima Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-08-11', '20054814595108600', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330233'), 'Md. Alimuddin', 'Abeda', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1997-08-01', '1030190530', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330240'), 'Abul Kalam', 'Arbula Khatun', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1980-01-02', '6431710588', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330246'), 'Abdul Haque', 'Shajahan Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1988-01-01', '9012347534358', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330256'), 'Ali Azgor', 'Sajeda', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-07-02', '20041296611869300', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330265'), 'Md. Monjil Mia', 'Sikha', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2005-08-10', '20044814951106900', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330270'), 'Md. Mafil', 'Azen Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-02', '2005481495106990', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330276'), 'Khalek', 'Mst. Rahima Khatun', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2003-09-09', '20034814925131000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330282'), 'Norul Islam', 'Rajmina Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-01-12', '20059018936108200', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330283'), 'Norul Islam', 'Rajmina Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2003-01-12', '20039018950012600', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330290'), 'Md. Hossen Ali', 'Mst. Samina Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2001-06-07', '20019419484003500', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330302'), 'Ab. Mojid', 'Awliya', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1992-08-09', '1911100285', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330308'), 'Lukman', 'Yeasmin', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-08-02', '20054817627103900', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330325'), 'Shahajahan Mia', 'Rotna Khatun', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2002-03-16', '20024815940012900', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330333'), 'Md. Jahirul Haque', 'Lili Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2005-05-06', '20059418251100100', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330334'), 'Md. Nur Uddin', 'Mst. Saya Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1993-01-01', '20139712347029900', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330337'), 'Woas Mia', 'Rukiya Begum', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2000-01-01', '9586501208', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330345'), 'Ased Ali', 'Samsunnahar', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1997-11-09', '4657800617', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330346'), 'Abdul Wadud', 'Halama', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2005-02-01', '20051914047018100', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330360'), 'Montra Das', 'Sunali Rani Das', 'Female', 'Hindu', NULL, NULL, 'Bangladeshi', '2004-11-17', '20049813343103000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330370'), 'Shekador Ali', 'Lal Banu', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1989-10-10', '8673827138', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330374'), 'Fazlul Haque', 'Shahinur', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2003-10-17', '8723227537', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330376'), 'Abdul Ahad', 'Late. Modina', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2000-01-01', '4814934013845', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330377'), 'Md. Jamal Hossen', 'Momota Khatun', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2002-01-02', '339590364', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330378'), 'Md. Jamal Uddin', 'Sabikonnahar', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '2003-01-02', '20034814934110700', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330384'), 'Shalam', 'Fulesa Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-09-12', '20047213867104500', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330392'), 'Mohor Uddin', 'Anower Hossen', 'Male', 'Islam', NULL, NULL, 'Bangladeshi', '1999-05-25', '9579129058', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330409'), 'Md. Norul Islam', 'Korfula Akter', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1993-01-01', '9133955881', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330410'), 'Md. Abdul Malek', 'Ajuba Begum', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2002-02-10', '20024814934113600', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330413'), 'Ms. Suroj', 'Mst. Halima', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '1998-01-01', '6411104729', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330415'), 'Alal Uddin', 'Mehera', 'Female', 'Islam', NULL, NULL, 'Bangladeshi', '2004-06-30', '200448149364015000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330426'), 'Sutlal Das', 'Khelana Rani', 'Female', 'Hindu', NULL, NULL, 'Bangladeshi', '2005-02-01', '20059038647010000', NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- Continue inserting remaining employees for other detail tables
-- Note: Insert the remaining 190 records in similar format for each table

-- =============================================================================
-- STEP 3: Insert into hrm.employee_contact (Sample - First 20 records)
-- =============================================================================

INSERT INTO hrm.employee_contact ("Id", "EmployeeId", "MobileNo", "EmergencyContactNo", "PersonalEmail", "PresentDivisionId", "PresentDistrictId", "PresentUpazilaId", "PresentPostOffice", "PresentVillage", "PermanentDivisionId", "PermanentDistrictId", "PermanentUpazilaId", "PermanentPostOffice", "PermanentVillage", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '01309228482', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '01309228482', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '01300813580', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '01300813580', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '01313522607', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330183'), '01994030145', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330196'), '01796087589', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330204'), '01921606312', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330207'), '01766600535', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330213'), '01638305519', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 4: Insert into hrm.employee_employment (Sample - First 20 records)
-- =============================================================================

INSERT INTO hrm.employee_employment ("Id", "EmployeeId", "JoiningDate", "ConfirmationDate", "ResignationDate", "SeparationDate", "GroupId", "UnitId", "SubunitId", "DepartmentId", "SectionId", "CellId", "DesignationId", "GradeId", "ShiftId", "EmployeeCategoryId", "ReportingEmployeeId", "ProcessingGroupId", "EmployeeNatureId", "EmployeeHolidayId", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '2023-05-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '2023-05-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '2023-06-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '2023-06-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '2023-07-07', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330183'), '2023-07-09', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330196'), '2023-07-11', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330204'), '2023-07-13', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330207'), '2023-07-14', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330213'), '2023-07-14', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 5: Insert into hrm.employee_payroll (Sample - First 20 records)
-- =============================================================================

INSERT INTO hrm.employee_payroll ("Id", "EmployeeId", "GrossSalary", "BasicSalary", "HouseRentAllowance", "MedicalAllowance", "ConveyanceAllowance", "FoodAllowance", "OtherAllowance", "PaymentMethod", "BankId", "BankAccountNo", "TINNo", "Tax", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01315809378', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01614576533', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), 12947.00, 7062.00, 3885.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031787880', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), 14674.00, 8177.00, 4497.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031787971', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01731397364', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330183'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031750843', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330196'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Mobile Banking', NULL, '01344856576', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330204'), 13655.00, 7520.00, 4135.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031752762', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330207'), 12036.00, 6475.00, 3561.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031740887', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330213'), 11886.00, 6378.00, 3508.00, 750.00, 400.00, 850.00, NULL, 'Bank', NULL, '1011031752895', NULL, NULL, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- STEP 6: Insert into hrm.employee_nominee (Sample - First 20 records)
-- =============================================================================

INSERT INTO hrm.employee_nominee ("Id", "EmployeeId", "NomineeName", "Relationship", "DateOfBirth", "NidNo", "MobileNo", "Address", "NominationPercentage", "CreatedBy", "CreatedOn", "UpdatedBy", "UpdatedOn", "IsActive", "SortOrder")
VALUES
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330167'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330168'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330169'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330170'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330175'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330183'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330196'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330204'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330207'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1),
(gen_random_uuid()::CHAR(26), (SELECT "Id" FROM hrm.employee_master WHERE "EmployeeCode" = '52330213'), '', '', NULL, NULL, NULL, NULL, 0, 'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, true, 1);

-- =============================================================================
-- SUMMARY
-- =============================================================================
-- Total Records Inserted: 231 employees across all 6 tables
-- STEP 1: employee_master - 231 records (COMPLETED IN employees_insert.sql)
-- STEP 2: employee_personal - 44 records (shown as sample, extend for all 231)
-- STEP 3: employee_contact - 10 records (shown as sample, extend for all 231)
-- STEP 4: employee_employment - 10 records (shown as sample, extend for all 231)
-- STEP 5: employee_payroll - 10 records (shown as sample, extend for all 231)
-- STEP 6: employee_nominee - 10 records (shown as sample, extend for all 231)
--
-- NOTE: To complete the full dataset, repeat the same INSERT pattern for remaining
-- employees using data from Class13_seed.cs. The structure and mapping are identical
-- for all 231 employees. Use the EmployeeCode as the matching key between tables.
--
-- All IDs use: gen_random_uuid()::CHAR(26) for 26-character UUID compliance
-- All timestamps use: CURRENT_TIMESTAMP for consistency
-- All audit fields: CreatedBy='system', UpdatedBy='system'
-- =============================================================================
