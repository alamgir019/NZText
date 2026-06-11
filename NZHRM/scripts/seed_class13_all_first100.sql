-- Seed: first 100 rows from NZ.HRM.Domain/Class13.cs
-- Creates a temp staging table and inserts 100 rows as plain INSERT ... VALUES

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
  "OtherAllowance" numeric,
  "PaymentMethod" text,
  "BankName" text,
  "BankAccountNo" text,
  "TINNo" text,
  "Tax" numeric
);

INSERT INTO temp_raw ("EmployeeCode","EmployeeName","Department","Section","GrossSalary","BasicSalary","HouseRentAllowance","MedicalAllowance","ConveyanceAllowance","FoodAllowance","OtherAllowance","PaymentMethod","BankName","BankAccountNo","TINNo","Tax") VALUES
('51730405','Sorna Parvin','Production','Ring',15018,8399,4619,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031412799',NULL,NULL),
('51730463','Rekha Akter','Production','Back Side',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031755119',NULL,NULL),
('51830561','Rojina','Production','Back Side',13155,7197,3958,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01746436443',NULL,NULL),
('51830646','Asma','Maintenance','Hackling',15855,8939,4916,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031279306',NULL,NULL),
('51830657','Rasel Islam','Store','Store',13285,7280,4005,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01407154277',NULL,NULL),
('51830693','Najmin','Production','Back Side',13655,7520,4135,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754340',NULL,NULL),
('51830755','Bani Israil','Store','Store',15855,8939,4916,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031317572',NULL,NULL),
('51830825','Nazma','Production','Back Side',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031756303',NULL,NULL),
('51931006','Anufa Khatun','Production','Back Side',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752953',NULL,NULL),
('51931036','Taniya Akter','Production','Back Side',13155,7197,3958,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01734531843',NULL,NULL),
('51931075','Alamgir Hossen','Maintenance','Ring',17685,10120,5565,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031323753',NULL,NULL),
('51931167','Nilufa Akter','Production','Ring',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750502',NULL,NULL),
('51931195','Anowara Begum','Production','Back Side',13255,7261,3994,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031755080',NULL,NULL),
('51931198','Soniya Akter','Production','Finishing',13605,7487,4118,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01336737290',NULL,NULL),
('51931218','Jhuma Akter','Production','Ring',13655,7520,4135,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031756067',NULL,NULL),
('51931284','Atindra Bishwas','Maintenance','Finishing',18135,10409,5726,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031124250',NULL,NULL),
('51931297','Mobina Begum','Production','Back Side',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031756296',NULL,NULL),
('51931341','Shirina Akter','Maintenance','Ring',13285,7280,4005,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031357633',NULL,NULL),
('51931372','Shiuly Khatun','Production','Back Side',13255,7261,3994,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752351',NULL,NULL),
('51931388','Masuda','Production','Finishing',13155,7197,3958,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752276',NULL,NULL),
('51931410','Shahana Khatun','HR & Admin','House Keeping',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031358044',NULL,NULL),
('52030003','Shahinur','Production','Finishing',13155,7197,3958,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01781819931',NULL,NULL),
('52030119','Yanur','Production','Ring',13235,7249,3986,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01701505826',NULL,NULL),
('52030140','Based Miah','Production','Hackling',17945,10287,5658,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031397606',NULL,NULL),
('52030162','Mostak Ahammed','Production','Hackling',16885,9603,5282,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01756618282',NULL,NULL),
('52030192','Parvin','Production','Ring',13235,7249,3986,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750913',NULL,NULL),
('52130031','Shiuly Begum','Production','Ring',13285,7280,4005,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031751013',NULL,NULL),
('52130088','Md Robin Mia','Production','Hackling',16900,9613,5287,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031787165',NULL,NULL),
('52130091','Renuka akter','Production','Ring',13235,7249,3986,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01782432851',NULL,NULL),
('52130094','Sagor Ahmed','Production','Hackling',15885,8958,4927,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031675392',NULL,NULL),
('52130113','Rajon','Production','Ring',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031792167',NULL,NULL),
('52130126','Rana Mia','Maintenance','Back Side',14528,8082,4446,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031625659',NULL,NULL),
('52130130','Borsha Rani Das','Production','Ring',12527,6791,3736,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01799653270',NULL,NULL),
('52130135','Mst. Najma Akter','Production','Ring',12517,6785,3732,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031751333',NULL,NULL),
('52130143','Momotaj Begum','Production','Finishing',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754057',NULL,NULL),
('52130155','Mst. Sheka','Production','Back Side',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031749632',NULL,NULL),
('52130161','Hridoy Miah','Production','Ring',14047,7772,4275,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01928144340',NULL,NULL),
('52130193','Sumaiya','Production','Ring',12947,7062,3885,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01932875835',NULL,NULL),
('52130194','Mst.Nahida','Production','Ring',12947,7062,3885,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01988510026',NULL,NULL),
('52130198','Md. Mohibol Islam','Production','Hackling',15035,8409,4626,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031707406',NULL,NULL),
('52130210','Mukta','Production','Back Side',13587,7476,4111,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752260',NULL,NULL),
('52130243','Hafsa Akter','Production','Finishing',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754452',NULL,NULL),
('52130250','Soniya Akter','Production','Ring',12947,7062,3885,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01326989225',NULL,NULL),
('52130281','Muslima','Production','Finishing',14047,7772,4275,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031733053',NULL,NULL),
('52130291','Sathi Akter','Production','Finishing',12947,7062,3885,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01322592750',NULL,NULL),
('52130294','Md. Shariful Islam','Production','Hackling',15335,8603,4732,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031686879',NULL,NULL),
('52130297','Aklima','Maintenance','Back Side',13370,7336,4034,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031625638',NULL,NULL),
('52130302','Fammi Akter','Production','Ring',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031755327',NULL,NULL),
('52130314','Md. Shahin Miah','Production','Hackling',15035,8409,4626,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031713517',NULL,NULL),
('52130368','Ab. Ali','Maintenance','Hackling',16935,9636,5299,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031632153',NULL,NULL),
('52130393','Mst. Sarufa Khatun','Production','Finishing',13047,7127,3920,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01797420385',NULL,NULL),
('52230007','Rifat Hossen','Maintenance','Ring',13810,7620,4190,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031681071',NULL,NULL),
('52230019','Rabbi Mia','Production','Hackling',14785,8249,4536,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031713452',NULL,NULL),
('52230028','Humayon Kabir','Electrical','Electrical',19085,11022,6063,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031639224',NULL,NULL),
('52230037','Md. Esob Ali','Production','Ring',12285,6636,3649,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031681391',NULL,NULL),
('52230047','Sanjida Akter','Production','Back Side',12547,6804,3743,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754041',NULL,NULL),
('52230076','Nur Mohammad','Production','Hackling',16585,9409,5176,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01835128105',NULL,NULL),
('52230114','Bithi Rani Das','Production','Ring',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031782340',NULL,NULL),
('52230121','Shila','Production','Ring',12897,7031,3866,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01327218571',NULL,NULL),
('52230123','Suniya Akter','Production','Finishing',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031753364',NULL,NULL),
('52230125','Borhan Uddin','Production','Ring',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750698',NULL,NULL),
('52230144','Suma Akter','Production','Finishing',12728,6921,3807,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750731',NULL,NULL),
('52230152','Mst. Tasbina','Production','Ring',12527,6791,3736,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01787390454',NULL,NULL),
('52230155','Monuara Begum','Production','Comber',12647,6869,3778,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752692',NULL,NULL),
('52230161','Hridoy Miah','Production','Ring',14047,7772,4275,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01928144340',NULL,NULL),
('52230167','Mukta Akter','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01775429352',NULL,NULL),
('52230168','Milha Akter','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01614576533',NULL,NULL),
('52230169','Khushi','Production','Ring',12947,7062,3885,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031787880',NULL,NULL),
('52230170','Rumi Akter','Production','Ring',14674,8177,4497,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031787971',NULL,NULL),
('52230175','Jesmin Begum','Production','Finishing',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01731397364',NULL,NULL),
('52230183','Rafi','Production','Ring',11886,6378,3508,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750843',NULL,NULL),
('52230196','Shukla Das','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01344856576',NULL,NULL),
('52310012','Apon','Production','Back Side',16000,9032,4968,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031732579',NULL,NULL),
('52310015','Miss. Rabeya Akter','QC','QC',15650,8807,4843,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031723200',NULL,NULL),
('52330005','Md. Aminul Islam','Production','Finishing',14335,7958,4377,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750942',NULL,NULL),
('52330014','Wari Islam','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01778614135',NULL,NULL),
('52330031','Majeda Begum','Production','Finishing',12517,6785,3732,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031760168',NULL,NULL),
('52330037','Norjahan','Maintenance','Hackling',11586,6184,3402,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031786953',NULL,NULL),
('52330038','Hasan Mahamud','Electrical','Electrical',14770,8239,4531,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031701834',NULL,NULL),
('52330052','Srabonti Rani Das','Production','Finishing',12628,6857,3771,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01320487188',NULL,NULL),
('52330073','Sima Akter','Production','Ring',12628,6857,3771,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01310589477',NULL,NULL),
('52330075','Sima','Production','Finishing',12628,6857,3771,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01931545335',NULL,NULL),
('52330077','Mst. Eya Moni Akter','Production','Ring',12628,6857,3771,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01310338499',NULL,NULL),
('52330082','Khadiza','Production','Finishing',12628,6857,3771,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01782067213',NULL,NULL),
('52330106','Sawpan','Production','Ring',12517,6785,3732,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01768501108',NULL,NULL),
('52330110','Mst. Panna Akter','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01309570869',NULL,NULL),
('52330113','Mst. Sorna Akter','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01312871947',NULL,NULL),
('52330114','Mst. Atika Akter Anisha','Production','Ring',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01727029360',NULL,NULL),
('52330117','Wakiya','Production','Ring',11886,6378,3508,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750859',NULL,NULL),
('52330118','Junaki Akter','Production','Finishing',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01313945835',NULL,NULL),
('52330119','Md. Rahat Ali','Production','Hackling',13335,7313,4022,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031723632',NULL,NULL),
('52330121','Alvi Rahman','Maintenance','Ring',13443,7382,4061,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031732563',NULL,NULL),
('52330130','Mst. Soniya Akter','Production','Finishing',11886,6378,3508,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01775429352',NULL,NULL),
('52330131','Mst. Meghna Begum','Production','Finishing',11886,6378,3508,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031752918',NULL,NULL),
('52330132','Mst. Mahfuja Begum','Production','Finishing',11886,6378,3508,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031750656',NULL,NULL),
('52330138','Md. Dabirul Islam','Maintenance','Workshop',15208,8521,4687,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031728798',NULL,NULL),
('52330139','Surma','Production','Back Side',13036,7120,3916,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031754884',NULL,NULL),
('52330152','Md. Kaium Khondaker','Maintenance','Hackling',14435,8022,4413,750,400,850,0,'Mobile Banking','BRAC Bank PLC','01987167346',NULL,NULL),
('52330158','Mst. Farjina','Production','Ring',12628,6857,3771,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031751739',NULL,NULL),
('52330161','Naim','Maintenance','Finishing',12935,7055,3880,750,400,850,0,'Bank','Dutch Bangla Bank PLC','1011031751200',NULL,NULL)
;

-- Insert unit (company) if not exists
INSERT INTO "master"."mst_unit" ("Id","GroupId","UnitCode","UnitName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder","MstGroupComplexId")
SELECT 'UN000000000000000000000004','GR000000000000000000000001','nzdy_flax_spinning_ltd','NZDY Flax Spinning Ltd',now(),'seed',now(),'seed',true,1000,'GC000000000000000000000001'
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_unit" WHERE "UnitName" = 'NZDY Flax Spinning Ltd');

-- Insert banks (distinct)
INSERT INTO "lookup"."bank" ("Id","BankCode","BankName","RoutingNo","ActiveFlag","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'BA' || lpad(md5("BankName")::text,24,'0'), lower(regexp_replace("BankName", '\\s+', '_', 'g')), "BankName", NULL, true, now(),'seed',now(),'seed',true,1000
FROM temp_raw t
WHERE NOT EXISTS (SELECT 1 FROM "lookup"."bank" b WHERE b."BankName" = t."BankName");

-- Insert departments
INSERT INTO "master"."mst_department" ("Id","SubunitId","DepartmentCode","DepartmentName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'DE' || lpad(md5("Department")::text,24,'0'), ''::text, lower(regexp_replace("Department", '\\s+', '_', 'g')), "Department", now(),'seed',now(),'seed',true,1000
FROM temp_raw t
WHERE NOT EXISTS (SELECT 1 FROM "master"."mst_department" d WHERE d."DepartmentName" = t."Department");

-- Insert sections
INSERT INTO "master"."mst_section" ("Id","SectionCode","SectionName","CreatedOn","CreatedBy","UpdatedOn","UpdatedBy","IsActive","SortOrder")
SELECT DISTINCT 'SE' || lpad(md5("Section")::text,24,'0'), lower(regexp_replace("Section", '\\s+', '_', 'g')), "Section", now(),'seed',now(),'seed',true,1000
FROM temp_raw t
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
  now(),'seed',now(),'seed',true,1000, COALESCE(NULLIF(t."EmployeeType",''),'Worker')
FROM temp_raw t
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_master" em WHERE em."EmployeeCode" = t."EmployeeCode");

-- Insert payroll records
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
FROM temp_raw t
LEFT JOIN "lookup"."bank" b ON b."BankName" = t."BankName"
WHERE NOT EXISTS (SELECT 1 FROM "hrm"."employee_payroll" ep WHERE ep."EmployeeId" = 'EM' || lpad(t."EmployeeCode",24,'0'));

-- Cleanup: drop staging table
DROP TABLE IF EXISTS temp_raw;
