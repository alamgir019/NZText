
-- Additional seed script for Class13 CSV data (COPY + staging)
-- Loads the full CSV embedded in Class13.cs into a temp staging table, then inserts into target tables.

-- Create staging table
CREATE TEMP TABLE IF NOT EXISTS temp_raw1 (
  "EmployeeCode" text,
  "EmployeeName" text,
  "Department" text,
  "Section" text,
  "GrossSalary" numeric,
  "BasicSalary" numeric,
  "HouseRentAllowance" numeric,
  "MedicalAllowance" numeric,
  "ConveyanceAllowance" numeric,
  "FoodAllowance" numeric,
  "OtherAllowance" numeric,
  "PaymentMethod" text,
  "BankName" text,
  "BankAccountNo" text,
  "TINNo" text,
  "Tax" numeric
);

-- Insert rows as plain INSERT ... VALUES (numbers cleaned)
INSERT INTO temp_raw1 ("EmployeeCode","EmployeeName","Department","Section","GrossSalary","BasicSalary","HouseRentAllowance","MedicalAllowance","ConveyanceAllowance","FoodAllowance","OtherAllowance","PaymentMethod","BankName","BankAccountNo","TINNo","Tax") VALUES
('101785','Md.Lijon Talukder','Safety & Security','Fire',16850,9580,5270,750,400,850,0,'Bank','Dutch Bangla Bank PLC','2361030018497','157690781713',420),
('103437','Tanvir Islam Dipu','Safety & Security','Fire',13250,7258,3992,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031701813','157690781714',420),
('50930003','Nazma','Production','Ring',14985,8378,4607,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031404144','157690781715',420),
('51230009','Shahin Miah','Store','Store',18035,10345,5690,750,400,850,0,'Bank','Dutch Bangla Bank PLC','2361030031968','157690781716',420),
('51330017','Shahjahan Miah','Production','Packing',14335,7958,4377,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031325326',NULL,NULL),
('51330026','Bilkis Begum','Production','Ring',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031751297',NULL,NULL),
('51330028','Tamanna Akter','Production','Ring',15468,8689,4779,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031397627',NULL,NULL),
('51430042','Kazol Miah','Maintenance','Finishing',17265,9849,5416,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031325693',NULL,NULL),
('51430046','Md. Abdul Rahman','Production','Ring',13840,7639,4201,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031317140',NULL,NULL),
('51530061','Mrs. Salma','Production','Ring',16141,9123,5018,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031392956',NULL,NULL),
('51630100','Rita Begum','Production','Packing',13635,7507,4128,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031358002',NULL,NULL),
('51730386','Joni Ater','Production','Back Side',14655,8164,4491,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031755241',NULL,NULL),
('51730405','Sorna Parvin','Production','Ring',15018,8399,4619,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031412799',NULL,NULL),
('51730463','Rekha Akter','Production','Back Side',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031755119',NULL,NULL),
('51830561','Rojina','Production','Back Side',13155,7197,3958,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01746436443',NULL,NULL),
('51830646','Asma','Maintenance','Hackling',15855,8939,4916,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031279306',NULL,NULL),
('51830657','Rasel Islam','Store','Store',13285,7280,4005,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01407154277',NULL,NULL),
('51830693','Najmin','Production','Back Side',13655,7520,4135,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754340',NULL,NULL),
('51830755','Bani Israil','Store','Store',15855,8939,4916,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031317572',NULL,NULL),
('51830825','Nazma','Production','Back Side',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031756303',NULL,NULL),
('52630102','Rubi Begum','Production','Ring',11732,6279,3453,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01760621700',NULL,NULL),
('52630103','Pospa Akter','Production','Ring',8005,3874,2131,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01718875843',NULL,NULL),
('52630105','sushama rani Das','Production','Comber',7505,3551,1954,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630107','Md.Rabbi Karbari','Production','Finishing',7505,3551,1954,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630109','Fatema Akter','Production','Finishing',8005,3874,2131,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01611334076',NULL,NULL),
('52630110','Mst.Tamanna','Production','Finishing',9000,4516,2484,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630111','Mst.Hawa Akter','Production','Ring',8005,3874,2131,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630112','Mostakim','Production','Hackling',9000,4516,2484,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01342788498',NULL,NULL),
('52630114','Md.Abu Tayeb','Production','Ring',14500,8064,4436,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01405253916',NULL,NULL),
('52630117','Siyam Mia','Production','Hackling',9000,4516,2484,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630118','Mst.Sumaiya','Production','Ring',8005,3874,2131,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630122','Md.Emon Sheikh','Store','Store',11000,5807,3193,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('102203','Zahidul','HR & Admin','Transport',12850,7000,3850,750,400,850,0,'Bank','Dutch Bangla Bank PLC','2361030038558',NULL,NULL),
('52010002','Suhag Miah','QC','QC',17540,10026,5514,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031226990',NULL,NULL),
('52010009','Riaj Mia','HR & Admin','Admin',16988,9670,5318,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031397686',NULL,NULL),
('52110014','Hakim MIah','HR & Admin','Admin',16750,9516,5234,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031601258',NULL,NULL),
('52210015','Jasim Uddin','Safety & Security','Fire',13250,7258,3992,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031701829',NULL,NULL),
('52310020','Md. Deloyar Hossen','Production','Hackling',13750,7580,4170,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031741270',NULL,NULL),
('52630123','Forhad Fahim Muntasir','Safety & Security','Fire',13250,7258,3992,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630124','Rima Akter','Production','Finishing',11570,6174,3396,750,400,850,0,'Cash',NULL,NULL,NULL,NULL),
('52630125','Konika','Production','Ring',8005,3874,2131,750,400,850,0,'Cash',NULL,NULL,NULL,NULL)
;

-- After COPY, transform staging data into target tables (example inserts)
-- Insert unit if not exists (same as other script)
INSERT INTO "master"."mst_unit" ("Id","GroupId","UnitCode","UnitName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder","MstGroupComplexId")
SELECT 'UN000000000000000000000004','GR000000000000000000000001','nzdy_flax_spinning_ltd','NZDY Flax Spinning Ltd',now(),'seed',now(),'seed',true,1000,'GC000000000000000000000001'
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_unit" WHERE "UnitName" = 'NZDY Flax Spinning Ltd');

-- Insert departments
INSERT INTO "master"."mst_department" ("Id","SubunitId","DepartmentCode","DepartmentName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'DE' || lpad(md5("Department")::text,24,'0'), ''::text, lower(regexp_replace("Department", '\\s+', '_', 'g')), "Department", now(),'seed',now(),'seed',true,1000
FROM temp_raw1 t
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" d WHERE d."DepartmentName" = t."Department");

-- Insert sections
INSERT INTO "master"."mst_section" ("Id","SectionCode","SectionName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'SE' || lpad(md5("Section")::text,24,'0'), lower(regexp_replace("Section", '\\s+', '_', 'g')), "Section", now(),'seed',now(),'seed',true,1000
FROM temp_raw1 t
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" s WHERE s."SectionName" = t."Section");

-- Insert employees
INSERT INTO "hrm"."employee_master" ("Id","EmployeeCode","EnrollmentId","CardNo","OldCardNo","EmployeeName","EmployeeNameBangla","EmployeeNameEnglish","MstPayrollProcessingGroupId","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder","EmployeeType")
SELECT DISTINCT
  'EM' || lpad(t."EmployeeCode",24,'0') AS "Id",
  t."EmployeeCode" AS "EmployeeCode",
  'EM' || lpad(t."EmployeeCode",24,'0') AS "EnrollmentId",
  'Card1' AS "CardNo",
  NULL AS "OldCardNo",
  t."EmployeeName" AS "EmployeeName",
  t."EmployeeName" AS "EmployeeNameBangla",
  t."EmployeeName" AS "EmployeeNameEnglish",
  'PPG0000000001' AS "MstPayrollProcessingGroupId",
  now(),'seed',now(),'seed',true,1000, 'Worker'
FROM temp_raw1 t
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" em WHERE em."EmployeeCode" = t."EmployeeCode");

-- Insert payroll
INSERT INTO "hrm"."employee_payroll" ("Id","EmployeeId","GrossSalary","BasicSalary","HouseRentAllowance","ConveyanceAllowance","MedicalAllowance","FoodAllowance","OtherAllowance","PaymentMethod","BankId","BankAccountNo","TINNo","Tax","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT
  'PR' || lpad(t."EmployeeCode",24,'0') AS "Id",
  'EM' || lpad(t."EmployeeCode",24,'0') AS "EmployeeId",
  COALESCE(t."GrossSalary",0), COALESCE(t."BasicSalary",0), COALESCE(t."HouseRentAllowance",0), COALESCE(t."ConveyanceAllowance",0), COALESCE(t."MedicalAllowance",0), COALESCE(t."FoodAllowance",0), COALESCE(t."OtherAllowance",0),
  t."PaymentMethod",
  b."Id" AS "BankId",
  t."BankAccountNo",
  t."TINNo",
  COALESCE(t."Tax",0),
  now(),'seed',now(),'seed',true,1000
FROM temp_raw1 t
LEFT JOIN "lookup"."bank" b ON b."BankName" = t."BankName"
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" ep WHERE ep."EmployeeId" = 'EM' || lpad(t."EmployeeCode",24,'0'));

-- Cleanup: drop staging table
DROP TABLE IF EXISTS temp_raw1;
