-- ============================================================================
-- DELETE DUPLICATE EMPLOYEES FROM employee_master TABLE
-- ============================================================================
-- Duplicate detection: By EmployeeCode
-- Keep: The oldest record (earliest CreatedOn date)
-- Delete: All other records with the same EmployeeCode
-- ============================================================================

-- ============================================================================
-- STEP 1: IDENTIFY DUPLICATES (Preview before deletion)
-- ============================================================================
-- Run this first to see which records will be deleted
SELECT 
	"EmployeeCode",
	COUNT(*) as duplicate_count,
	STRING_AGG("Id"::text, ', ') as all_ids,
	MIN("CreatedOn") as oldest_record_date,
	MAX("CreatedOn") as newest_record_date
FROM hrm.employee_master
GROUP BY "EmployeeCode"
HAVING COUNT(*) > 1
ORDER BY "EmployeeCode";

-- ============================================================================
-- STEP 2: VIEW DUPLICATE RECORDS IN DETAIL (Optional - for verification)
-- ============================================================================
-- Shows all duplicate records with details
SELECT 
	em."Id",
	em."EmployeeCode",
	em."EmployeeName",
	em."CreatedOn",
	em."CreatedBy",
	em."UpdatedOn",
	ROW_NUMBER() OVER (PARTITION BY em."EmployeeCode" ORDER BY em."CreatedOn" ASC) as row_num,
	CASE 
		WHEN ROW_NUMBER() OVER (PARTITION BY em."EmployeeCode" ORDER BY em."CreatedOn" ASC) = 1 
		THEN 'KEEP (Oldest)'
		ELSE 'DELETE'
	END as action
FROM hrm.employee_master em
WHERE em."EmployeeCode" IN (
	SELECT "EmployeeCode" 
	FROM hrm.employee_master 
	GROUP BY "EmployeeCode" 
	HAVING COUNT(*) > 1
)
ORDER BY em."EmployeeCode", em."CreatedOn";

-- ============================================================================
-- STEP 3: COUNT DUPLICATE RECORDS BEFORE DELETION
-- ============================================================================
SELECT 
	COUNT(*) as total_duplicate_records_to_delete
FROM (
	SELECT 
		em."Id",
		ROW_NUMBER() OVER (PARTITION BY em."EmployeeCode" ORDER BY em."CreatedOn" ASC) as row_num
	FROM hrm.employee_master em
) duplicates
WHERE row_num > 1;

-- ============================================================================
-- STEP 4: DELETE DUPLICATE RECORDS
-- ============================================================================
-- APPROACH 1: Using CTE with ROW_NUMBER (Recommended - PostgreSQL 12+)
-- ============================================================================
DELETE FROM hrm.employee_master
WHERE "Id" IN (
	SELECT "Id"
	FROM (
		SELECT 
			"Id",
			ROW_NUMBER() OVER (PARTITION BY "EmployeeCode" ORDER BY "CreatedOn" ASC) as row_num
		FROM hrm.employee_master
	) ranked
	WHERE row_num > 1
);

-- ============================================================================
-- ALTERNATIVE APPROACH 2: Using NOT IN with subquery
-- ============================================================================
-- Uncomment below if APPROACH 1 doesn't work
/*
DELETE FROM hrm.employee_master
WHERE "Id" NOT IN (
	SELECT MIN("Id")
	FROM hrm.employee_master
	GROUP BY "EmployeeCode"
);
*/

-- ============================================================================
-- STEP 5: VERIFY DELETION - NO DUPLICATES SHOULD EXIST
-- ============================================================================
-- Run after deletion to confirm all duplicates are removed
SELECT 
	"EmployeeCode",
	COUNT(*) as record_count
FROM hrm.employee_master
GROUP BY "EmployeeCode"
HAVING COUNT(*) > 1;

-- If this query returns NO ROWS, the deletion was successful ✓

-- ============================================================================
-- STEP 6: FINAL STATISTICS
-- ============================================================================
SELECT 
	COUNT(*) as total_employees,
	COUNT(DISTINCT "EmployeeCode") as unique_employee_codes,
	CASE 
		WHEN COUNT(*) = COUNT(DISTINCT "EmployeeCode") THEN 'NO DUPLICATES ✓'
		ELSE 'DUPLICATES EXIST ✗'
	END as duplicate_status
FROM hrm.employee_master;

-- ============================================================================
-- CLEANUP: DELETE ORPHANED RECORDS FROM DETAIL TABLES (Optional)
-- ============================================================================
-- If you have foreign key constraints, orphaned detail records will be
-- deleted automatically if ON DELETE CASCADE is enabled.
-- Otherwise, run the queries below to clean up orphaned records:

-- Check for orphaned employee_personal records
SELECT COUNT(*) as orphaned_personal_records
FROM hrm.employee_personal ep
WHERE NOT EXISTS (
	SELECT 1 FROM hrm.employee_master em WHERE em."Id" = ep."EmployeeId"
);

-- Check for orphaned employee_contact records
SELECT COUNT(*) as orphaned_contact_records
FROM hrm.employee_contact ec
WHERE NOT EXISTS (
	SELECT 1 FROM hrm.employee_master em WHERE em."Id" = ec."EmployeeId"
);

-- Check for orphaned employee_employment records
SELECT COUNT(*) as orphaned_employment_records
FROM hrm.employee_employment ee
WHERE NOT EXISTS (
	SELECT 1 FROM hrm.employee_master em WHERE em."Id" = ee."EmployeeId"
);

-- Check for orphaned employee_payroll records
SELECT COUNT(*) as orphaned_payroll_records
FROM hrm.employee_payroll ep
WHERE NOT EXISTS (
	SELECT 1 FROM hrm.employee_master em WHERE em."Id" = ep."EmployeeId"
);

-- Check for orphaned employee_nominee records
SELECT COUNT(*) as orphaned_nominee_records
FROM hrm.employee_nominee en
WHERE NOT EXISTS (
	SELECT 1 FROM hrm.employee_master em WHERE em."Id" = en."EmployeeId"
);

-- ============================================================================
-- OPTIONAL: DELETE ORPHANED RECORDS
-- ============================================================================
-- Uncomment to delete orphaned records (only if no CASCADE constraints)
/*
DELETE FROM hrm.employee_personal
WHERE "EmployeeId" NOT IN (SELECT "Id" FROM hrm.employee_master);

DELETE FROM hrm.employee_contact
WHERE "EmployeeId" NOT IN (SELECT "Id" FROM hrm.employee_master);

DELETE FROM hrm.employee_employment
WHERE "EmployeeId" NOT IN (SELECT "Id" FROM hrm.employee_master);

DELETE FROM hrm.employee_payroll
WHERE "EmployeeId" NOT IN (SELECT "Id" FROM hrm.employee_master);

DELETE FROM hrm.employee_nominee
WHERE "EmployeeId" NOT IN (SELECT "Id" FROM hrm.employee_master);
*/

-- ============================================================================
-- END OF DUPLICATE DELETION SCRIPT
-- ============================================================================
