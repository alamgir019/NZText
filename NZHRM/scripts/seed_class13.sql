-- Seed script for Class13 CSV data (partial sample rows)
-- Uses PascalCase column names matching entity models and quoted identifiers for PostgreSQL

-- create a temp table to hold CSV rows so it can be referenced by multiple statements
CREATE TEMP TABLE IF NOT EXISTS temp_raw (
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
	"OtherAllowance" numeric DEFAULT 0,
  "PaymentMethod" text,
  "BankName" text,
  "BankAccountNo" text,
  "TINNo" text,
  "Tax" numeric
);

INSERT INTO temp_raw ("EmployeeCode","EmployeeName","Department","Section","GrossSalary","BasicSalary","HouseRentAllowance","MedicalAllowance","ConveyanceAllowance","FoodAllowance","OtherAllowance","PaymentMethod","BankName","BankAccountNo","TINNo","Tax") VALUES
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
	('51630100','Rita Begum','Production','Packing',13635,7507,4128,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031358002',NULL,NULL);

-- Insert unit (company) if not exists
INSERT INTO "master"."mst_unit" ("Id","GroupId","UnitCode","UnitName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder","MstGroupComplexId")
SELECT 'UN000000000000000000000004','GR000000000000000000000001','nzdy_flax_spinning_ltd','NZDY Flax Spinning Ltd',now(),'seed',now(),'seed',true,1000,'GC000000000000000000000001'
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_unit" WHERE "UnitName" = 'NZDY Flax Spinning Ltd');

-- Insert banks
INSERT INTO "lookup"."bank" ("Id","BankCode","BankName","RoutingNo","ActiveFlag","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT 'BA000000000000000000000001','DBBL','Dutch Bangla Bank PLC',NULL,true,now(),'seed',now(),'seed',true,1000
WHERE NOT EXISTS (SELECT 1 FROM "lookup"."bank" WHERE "BankName" = 'Dutch Bangla Bank PLC');

-- Insert distinct departments
INSERT INTO "master"."mst_department" ("Id","SubunitId","DepartmentCode","DepartmentName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'DE' || lpad(md5("Department")::text,24,'0'), ''::text, "Department", "Department", now(),'seed',now(),'seed',true,1000
FROM (
  SELECT DISTINCT "Department" FROM temp_raw
) s
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" d WHERE d."DepartmentName" = s."Department");

-- Insert distinct sections
INSERT INTO "master"."mst_section" ("Id","SectionCode","SectionName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'SE' || lpad(md5("Section")::text,24,'0'), "Section", "Section", now(),'seed',now(),'seed',true,1000
FROM (
  SELECT DISTINCT "Section" FROM temp_raw
) s
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_section" sec WHERE sec."SectionName" = s."Section");

-- Insert employees
INSERT INTO "hrm"."employee_master" ("Id","EmployeeCode","EnrollmentId","CardNo","OldCardNo","EmployeeName","EmployeeNameBangla","EmployeeNameEnglish","MstPayrollProcessingGroupId","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder","EmployeeType")
SELECT
  'EM' || lpad(r."EmployeeCode",24,'0') AS "Id",
	r."EmployeeCode",
  'EM' || lpad(r."EmployeeCode",24,'0') AS "EnrollmentId",
  'Card1' AS "CardNo",
  NULL AS "OldCardNo",
  r."EmployeeName",
  r."EmployeeName",
  r."EmployeeName",
  'PPG0000000001' AS "MstPayrollProcessingGroupId",
  now(),'seed',now(),'seed',true,1000,'Worker'
FROM temp_raw r
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" em WHERE em."EmployeeCode" = r."EmployeeCode");

-- Insert payroll records joining to bank and employee
INSERT INTO "hrm"."employee_payroll" ("Id","EmployeeId","GrossSalary","BasicSalary","HouseRentAllowance","ConveyanceAllowance","MedicalAllowance","FoodAllowance","OtherAllowance","PaymentMethod","BankId","BankAccountNo","TINNo","Tax","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT
  'PR' || lpad(r."EmployeeCode",24,'0') AS "Id",
  'EM' || lpad(r."EmployeeCode",24,'0') AS "EmployeeId",
  r."GrossSalary", r."BasicSalary", r."HouseRentAllowance", r."ConveyanceAllowance", r."MedicalAllowance", r."FoodAllowance", r."OtherAllowance",
  r."PaymentMethod",
  b."Id" AS "BankId",
  r."BankAccountNo",
  r."TINNo",
  r."Tax",
  now(),'seed',now(),'seed',true,1000
FROM temp_raw r
LEFT JOIN "lookup"."bank" b ON b."BankName" = r."BankName"
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" ep WHERE ep."EmployeeId" = 'EM' || lpad(r."EmployeeCode",24,'0'));

-- End of seed script (partial sample)
