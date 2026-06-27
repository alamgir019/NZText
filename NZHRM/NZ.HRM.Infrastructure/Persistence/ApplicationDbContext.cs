using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Master
        public DbSet<MstGroup> MstGroups => Set<MstGroup>();
        public DbSet<MstUnit> MstUnits => Set<MstUnit>();
        public DbSet<MstSubunit> MstSubunits => Set<MstSubunit>();
        public DbSet<MstDepartment> MstDepartments => Set<MstDepartment>();
        public DbSet<MstSection> MstSections => Set<MstSection>();
        public DbSet<MstCell> MstCells => Set<MstCell>();
        public DbSet<MstDesignation> MstDesignations => Set<MstDesignation>();
        public DbSet<MstGrade> MstGrades => Set<MstGrade>();
        public DbSet<MstShift> MstShifts => Set<MstShift>();
        public DbSet<MstEmployeeCategory> MstEmployeeCategories => Set<MstEmployeeCategory>();
        public DbSet<MstPayrollProcessingGroup> MstPayrollProcessingGroups => Set<MstPayrollProcessingGroup>();
        public DbSet<MstDepartmentUnitComplex> MstDepartmentUnitComplexes => Set<MstDepartmentUnitComplex>();

        // lookup
        public DbSet<LookDivision> Divisions => Set<LookDivision>();
        public DbSet<LookDistrict> Districts => Set<LookDistrict>();
        public DbSet<LookThana> Thanas => Set<LookThana>();
        public DbSet<LookEmployeeNature> EmployeeNatures => Set<LookEmployeeNature>();
        public DbSet<LookBanking> Banks => Set<LookBanking>();

        // HRM
        public DbSet<HrmEmployeeVerification> HrmEmployeeVerifications => Set<HrmEmployeeVerification>();
        public DbSet<HrmMedicalFitnessCheck> HrmMedicalFitnessChecks => Set<HrmMedicalFitnessCheck>();
        public DbSet<HrmPhysicalExaminationSetting> HrmPhysicalExaminationSettings => Set<HrmPhysicalExaminationSetting>();
        public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();
        public DbSet<HrmEmployeePersonal> HrmEmployeePersonals => Set<HrmEmployeePersonal>();
        public DbSet<HrmEmployeeContact> HrmEmployeeContacts => Set<HrmEmployeeContact>();
        public DbSet<HrmEmployeeEmployment> HrmEmployeeEmployments => Set<HrmEmployeeEmployment>();
        public DbSet<HrmEmployeePayroll> HrmEmployeePayrolls => Set<HrmEmployeePayroll>();
        public DbSet<HrmEmployeeDocument> HrmEmployeeDocuments => Set<HrmEmployeeDocument>();
        public DbSet<HrmEmployeeNominee> HrmEmployeeNominees => Set<HrmEmployeeNominee>();
        public DbSet<HrmEmployeeEducation> HrmEmployeeEducations => Set<HrmEmployeeEducation>();
        public DbSet<HrmEmployeeExperience> HrmEmployeeExperiences => Set<HrmEmployeeExperience>();
        public DbSet<HrmEmployeeTraining> HrmEmployeeTrainings => Set<HrmEmployeeTraining>();
        public DbSet<HrmEmployeeFamily> HrmEmployeeFamilies => Set<HrmEmployeeFamily>();
        public DbSet<HrmEmployeeSalaryAccount> HrmEmployeeBankAccounts => Set<HrmEmployeeSalaryAccount>();
        public DbSet<HrmEmployeeReporting> HrmEmployeeReportings => Set<HrmEmployeeReporting>();

        // Attendance
        public DbSet<AttDeviceMaster> AttDeviceMasters => Set<AttDeviceMaster>();
        public DbSet<AttDeviceSyncLog> AttDeviceSyncLogs => Set<AttDeviceSyncLog>();
        public DbSet<AttRawPunch> AttRawPunches => Set<AttRawPunch>();
        public DbSet<AttProcessedPunch> AttProcessedPunches => Set<AttProcessedPunch>();
        public DbSet<AttShiftRoster> AttShiftRosters => Set<AttShiftRoster>();
        public DbSet<AttOtAuthorization> AttOtAuthorizations => Set<AttOtAuthorization>();
        public DbSet<AttProcessedAttendance> AttProcessedAttendances => Set<AttProcessedAttendance>();
        public DbSet<AttAttendanceException> AttAttendanceExceptions => Set<AttAttendanceException>();
        public DbSet<AttAttendanceAdjustment> AttAttendanceAdjustments => Set<AttAttendanceAdjustment>();
        public DbSet<AttAttendanceLock> AttAttendanceLocks => Set<AttAttendanceLock>();
        public DbSet<AttProcessingLog> AttProcessingLogs => Set<AttProcessingLog>();
        public DbSet<AttInsideFactoryStatus> AttInsideFactoryStatuses => Set<AttInsideFactoryStatus>();
        public DbSet<AttWeeklyOffPattern> AttWeeklyOffPatterns => Set<AttWeeklyOffPattern>();

        // Leave
        public DbSet<LevLeaveType> LevLeaveTypes => Set<LevLeaveType>();
        public DbSet<LevLeaveBalance> LevLeaveBalances => Set<LevLeaveBalance>();
        public DbSet<LevLeaveApplication> LevLeaveApplications => Set<LevLeaveApplication>();
        public DbSet<LevLeaveApplicationDetails> LevLeaveApplicationDetails => Set<LevLeaveApplicationDetails>();
        public DbSet<LevLeaveAdjustment> LevLeaveAdjustments => Set<LevLeaveAdjustment>();
        public DbSet<LevLeaveOpeningBalance> LevLeaveOpeningBalances => Set<LevLeaveOpeningBalance>();
        public DbSet<LevLeaveEncashment> LevLeaveEncashments => Set<LevLeaveEncashment>();
        public DbSet<LevLeaveAccrual> LevLeaveAccruals => Set<LevLeaveAccrual>();
        public DbSet<LevHolidayCalendar> LevHolidayCalendars => Set<LevHolidayCalendar>();
        public DbSet<LevLeaveApprovalHistory> LevLeaveApprovalHistories => Set<LevLeaveApprovalHistory>();
        public DbSet<LevLeaveCancellation> LevLeaveCancellations => Set<LevLeaveCancellation>();
        public DbSet<LevLeaveYear> LevLeaveYears => Set<LevLeaveYear>();
        public DbSet<LevLeavePolicy> LevLeavePolicies => Set<LevLeavePolicy>();

        // Payroll
        public DbSet<PaySalaryStructure> PaySalaryStructures => Set<PaySalaryStructure>();
        public DbSet<PayIncrementHistory> PayIncrementHistories => Set<PayIncrementHistory>();
        public DbSet<PayPayrollHeader> PayPayrollHeaders => Set<PayPayrollHeader>();
        public DbSet<PayPayrollDetails> PayPayrollDetails => Set<PayPayrollDetails>();
        public DbSet<PayOtDetails> PayOtDetails => Set<PayOtDetails>();
        public DbSet<PayDeduction> PayDeductions => Set<PayDeduction>();
        public DbSet<PayArrear> PayArrears => Set<PayArrear>();
        public DbSet<PayBonus> PayBonuses => Set<PayBonus>();
        public DbSet<PayTax> PayTaxes => Set<PayTax>();
        public DbSet<PayLoanRecovery> PayLoanRecoveries => Set<PayLoanRecovery>();
        public DbSet<PayBankTransfer> PayBankTransfers => Set<PayBankTransfer>();
        public DbSet<PayPayslip> PayPayslips => Set<PayPayslip>();
        public DbSet<PayPayrollAdjustment> PayPayrollAdjustments => Set<PayPayrollAdjustment>();
        public DbSet<PayPayrollLock> PayPayrollLocks => Set<PayPayrollLock>();
        public DbSet<PayPayrollProcessLog> PayPayrollProcessLogs => Set<PayPayrollProcessLog>();
        public DbSet<PayPartialSalaryPayment> PayPartialSalaryPayments => Set<PayPartialSalaryPayment>();
        public DbSet<PaySpecialPayrollPolicy> PaySpecialPayrollPolicies => Set<PaySpecialPayrollPolicy>();
        public DbSet<PaySpecialPayrollBand> PaySpecialPayrollBands => Set<PaySpecialPayrollBand>();
        public DbSet<PayPayrollException> PayPayrollExceptions => Set<PayPayrollException>();

        // Workflow
        public DbSet<WfWorkflowMaster> WfWorkflowMasters => Set<WfWorkflowMaster>();
        public DbSet<WfWorkflowStep> WfWorkflowSteps => Set<WfWorkflowStep>();
        public DbSet<WfWorkflowTransaction> WfWorkflowTransactions => Set<WfWorkflowTransaction>();
        public DbSet<WfApprovalHistory> WfApprovalHistories => Set<WfApprovalHistory>();
        public DbSet<WfPendingApproval> WfPendingApprovals => Set<WfPendingApproval>();
        public DbSet<WfDelegation> WfDelegations => Set<WfDelegation>();
        public DbSet<WfEscalationRule> WfEscalationRules => Set<WfEscalationRule>();
        public DbSet<WfNotificationQueue> WfNotificationQueues => Set<WfNotificationQueue>();
        public DbSet<WfWorkflowAttachment> WfWorkflowAttachments => Set<WfWorkflowAttachment>();
        public DbSet<WfWorkflowAudit> WfWorkflowAudits => Set<WfWorkflowAudit>();

        // Security & Audit
        public DbSet<SecUser> SecUsers => Set<SecUser>();
        public DbSet<SecRole> SecRoles => Set<SecRole>();
        public DbSet<SecUserRole> SecUserRoles => Set<SecUserRole>();
        public DbSet<SecUserSession> SecUserSessions => Set<SecUserSession>();
        public DbSet<SecPermission> SecPermissions => Set<SecPermission>();
        public DbSet<SecRolePermission> SecRolePermissions => Set<SecRolePermission>();
        public DbSet<SecPasswordHistory> SecPasswordHistories => Set<SecPasswordHistory>();
        public DbSet<SecModuleAccess> SecModuleAccesses => Set<SecModuleAccess>();
        public DbSet<SecFieldSecurity> SecFieldSecurities => Set<SecFieldSecurity>();
        public DbSet<SecEmergencyAccess> SecEmergencyAccesses => Set<SecEmergencyAccess>();

        public DbSet<AudLoginHistory> AudLoginHistories => Set<AudLoginHistory>();
        public DbSet<AudDataChange> AudDataChanges => Set<AudDataChange>();
        public DbSet<AudApprovalTrail> AudApprovalTrails => Set<AudApprovalTrail>();
        public DbSet<AudReportAccess> AudReportAccesses => Set<AudReportAccess>();
        public DbSet<AudExportHistory> AudExportHistories => Set<AudExportHistory>();
        public DbSet<AudSystemEvent> AudSystemEvents => Set<AudSystemEvent>();

        // Recruitment
        public DbSet<RecManpowerRequisition> RecManpowerRequisitions => Set<RecManpowerRequisition>();
        public DbSet<RecJobPosition> RecJobPositions => Set<RecJobPosition>();
        public DbSet<RecCandidate> RecCandidates => Set<RecCandidate>();
        public DbSet<RecCandidateDocument> RecCandidateDocuments => Set<RecCandidateDocument>();
        public DbSet<RecInterviewSchedule> RecInterviewSchedules => Set<RecInterviewSchedule>();
        public DbSet<RecInterviewEvaluation> RecInterviewEvaluations => Set<RecInterviewEvaluation>();
        public DbSet<RecInterviewPanel> RecInterviewPanels => Set<RecInterviewPanel>();
        public DbSet<RecOfferLetter> RecOfferLetters => Set<RecOfferLetter>();
        public DbSet<RecAppointment> RecAppointments => Set<RecAppointment>();
        public DbSet<RecRecruitmentWorkflow> RecRecruitmentWorkflows => Set<RecRecruitmentWorkflow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Security
            //modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<SecUser>().ToTable("user_account", "security");
            //modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<SecRole>().ToTable("role", "security");
            modelBuilder.Entity<SecUserRole>().ToTable("user_role", "security");
            modelBuilder.Entity<SecUserSession>().ToTable("user_session", "security");
            modelBuilder.Entity<SecPermission>().ToTable("permission", "security");
            modelBuilder.Entity<SecRolePermission>().ToTable("role_permission", "security");
            modelBuilder.Entity<SecPasswordHistory>().ToTable("password_history", "security");
            modelBuilder.Entity<SecModuleAccess>().ToTable("module_access", "security");
            modelBuilder.Entity<SecFieldSecurity>().ToTable("field_security", "security");
            modelBuilder.Entity<SecEmergencyAccess>().ToTable("emergency_access", "security");

            // Master
            modelBuilder.Entity<MstGroup>().ToTable("mst_group", "master");
            modelBuilder.Entity<MstUnit>().ToTable("mst_unit", "master");
            modelBuilder.Entity<MstSubunit>().ToTable("mst_subunit", "master");
            modelBuilder.Entity<MstDepartment>().ToTable("mst_department", "master");
            modelBuilder.Entity<MstSection>().ToTable("mst_section", "master");
            modelBuilder.Entity<MstCell>().ToTable("mst_cell", "master");
            modelBuilder.Entity<MstDesignation>().ToTable("mst_designation", "master");
            modelBuilder.Entity<MstGrade>().ToTable("mst_grade", "master");
            modelBuilder.Entity<MstShift>().ToTable("mst_shift", "master");
            modelBuilder.Entity<MstEmployeeCategory>().ToTable("mst_employee_category", "master");
            modelBuilder.Entity<MstPayrollProcessingGroup>().ToTable("payroll_processing_group", "master");
            modelBuilder.Entity<MstDepartmentUnitComplex>().ToTable("mst_department_unit_complex", "master");
            //modelBuilder.Entity<MstDepartmentSection>().ToTable("mst_department_section", "master");

            // HRM
            modelBuilder.Entity<HrmEmployeeMaster>().ToTable("employee_master", "hrm");
            modelBuilder.Entity<HrmEmployeePersonal>().ToTable("employee_personal", "hrm");
            modelBuilder.Entity<HrmEmployeeContact>().ToTable("employee_contact", "hrm");
            modelBuilder.Entity<HrmEmployeeEmployment>().ToTable("employee_employment", "hrm");
            modelBuilder.Entity<HrmEmployeePayroll>().ToTable("employee_payroll", "hrm");
            modelBuilder.Entity<HrmEmployeeDocument>().ToTable("employee_document", "hrm");
            modelBuilder.Entity<HrmEmployeeNominee>().ToTable("employee_nominee", "hrm");
            modelBuilder.Entity<HrmEmployeeEducation>().ToTable("employee_education", "hrm");
            modelBuilder.Entity<HrmEmployeeExperience>().ToTable("employee_experience", "hrm");
            modelBuilder.Entity<HrmEmployeeTraining>().ToTable("employee_training", "hrm");
            modelBuilder.Entity<HrmEmployeeFamily>().ToTable("employee_family", "hrm");
            modelBuilder.Entity<HrmEmployeeSalaryAccount>().ToTable("employee_bank_account", "hrm");
            modelBuilder.Entity<HrmEmployeeReporting>().ToTable("employee_reporting", "hrm");
            modelBuilder.Entity<HrmEmployeeVerification>().ToTable("employee_verification", "hrm");
            modelBuilder.Entity<HrmMedicalFitnessCheck>().ToTable("medical_fitness_check", "hrm");
            modelBuilder.Entity<HrmPhysicalExaminationSetting>().ToTable("physical_examination_setting", "hrm");

            // HRM one-to-one mappings: explicit configuration for Employee related one-to-one sections
            modelBuilder.Entity<HrmEmployeeMaster>()
                .HasOne(e => e.Personal)
                .WithOne(p => p.Employee)
                .HasForeignKey<HrmEmployeePersonal>(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HrmEmployeeMaster>()
                .HasOne(e => e.Contact)
                .WithOne(c => c.Employee)
                .HasForeignKey<HrmEmployeeContact>(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HrmEmployeeMaster>()
                .HasOne(e => e.Employment)
                .WithOne(emp => emp.Employee)
                .HasForeignKey<HrmEmployeeEmployment>(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HrmEmployeeMaster>()
                .HasOne(e => e.Payroll)
                .WithOne(p => p.Employee)
                .HasForeignKey<HrmEmployeePayroll>(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reporting relationships: HrmEmployeeReporting has two FKs to HrmEmployeeMaster
            modelBuilder.Entity<HrmEmployeeReporting>()
                .HasOne(r => r.ReportingEmployee)
                .WithMany(e => e.Reportings)
                .HasForeignKey(r => r.ReportingEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HrmEmployeeReporting>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attendance
            modelBuilder.Entity<AttDeviceMaster>().ToTable("device_master", "attendance");
            modelBuilder.Entity<AttDeviceSyncLog>().ToTable("device_sync_log", "attendance");
            modelBuilder.Entity<AttRawPunch>().ToTable("raw_punch", "attendance");
            modelBuilder.Entity<AttProcessedPunch>().ToTable("processed_punches", "attendance");
            modelBuilder.Entity<AttShiftRoster>().ToTable("shift_roster", "attendance");
            modelBuilder.Entity<AttOtAuthorization>().ToTable("ot_authorization", "attendance");
            modelBuilder.Entity<AttProcessedAttendance>().ToTable("processed_attendance", "attendance");
            modelBuilder.Entity<AttAttendanceException>().ToTable("attendance_exception", "attendance");
            modelBuilder.Entity<AttAttendanceAdjustment>().ToTable("attendance_adjustment", "attendance");
            modelBuilder.Entity<AttAttendanceLock>().ToTable("attendance_lock", "attendance");
            modelBuilder.Entity<AttProcessingLog>().ToTable("processing_log", "attendance");
            modelBuilder.Entity<AttInsideFactoryStatus>().ToTable("inside_factory_status", "attendance");
            modelBuilder.Entity<AttWeeklyOffPattern>().ToTable("weekly_off_pattern", "attendance");

            // Leave
            modelBuilder.Entity<LevLeaveType>().ToTable("leave_type", "leave_mgmt");
            modelBuilder.Entity<LevLeaveBalance>().ToTable("leave_balance", "leave_mgmt");
            modelBuilder.Entity<LevLeaveApplication>().ToTable("leave_application", "leave_mgmt");
            modelBuilder.Entity<LevLeaveApplicationDetails>().ToTable("leave_application_details", "leave_mgmt");
            modelBuilder.Entity<LevLeaveAdjustment>().ToTable("leave_adjustment", "leave_mgmt");
            modelBuilder.Entity<LevLeaveOpeningBalance>().ToTable("leave_opening_balance", "leave_mgmt");
            modelBuilder.Entity<LevLeaveEncashment>().ToTable("leave_encashment", "leave_mgmt");
            modelBuilder.Entity<LevLeaveAccrual>().ToTable("leave_accrual", "leave_mgmt");
            modelBuilder.Entity<LevHolidayCalendar>().ToTable("holiday_calendar", "leave_mgmt");
            modelBuilder.Entity<LevLeaveApprovalHistory>().ToTable("leave_approval_history", "leave_mgmt");
            modelBuilder.Entity<LevLeaveCancellation>().ToTable("leave_cancellation", "leave_mgmt");
            modelBuilder.Entity<LevLeaveYear>().ToTable("leave_year", "leave_mgmt");
            modelBuilder.Entity<LevLeavePolicy>().ToTable("leave_policy", "leave_mgmt");

            // Payroll
            modelBuilder.Entity<PaySalaryStructure>().ToTable("salary_structure", "payroll");
            modelBuilder.Entity<PayIncrementHistory>().ToTable("increment_history", "payroll");
            modelBuilder.Entity<PayPayrollHeader>().ToTable("payroll_header", "payroll");
            modelBuilder.Entity<PayPayrollDetails>().ToTable("payroll_details", "payroll");
            modelBuilder.Entity<PayOtDetails>().ToTable("ot_details", "payroll");
            modelBuilder.Entity<PayDeduction>().ToTable("deduction", "payroll");
            modelBuilder.Entity<PayArrear>().ToTable("arrear", "payroll");
            modelBuilder.Entity<PayBonus>().ToTable("bonus", "payroll");
            modelBuilder.Entity<PayTax>().ToTable("tax", "payroll");
            modelBuilder.Entity<PayLoanRecovery>().ToTable("loan_recovery", "payroll");
            modelBuilder.Entity<PayBankTransfer>().ToTable("bank_transfer", "payroll");
            modelBuilder.Entity<PayPayslip>().ToTable("payslip", "payroll");
            modelBuilder.Entity<PayPayrollAdjustment>().ToTable("payroll_adjustment", "payroll");
            modelBuilder.Entity<PayPayrollLock>().ToTable("payroll_lock", "payroll");
            modelBuilder.Entity<PayPayrollProcessLog>().ToTable("payroll_process_log", "payroll");
            modelBuilder.Entity<PayPartialSalaryPayment>().ToTable("partial_salary_payment", "payroll");
            modelBuilder.Entity<PaySpecialPayrollPolicy>().ToTable("special_payroll_policy", "payroll");
            modelBuilder.Entity<PaySpecialPayrollBand>().ToTable("special_payroll_band", "payroll");
            modelBuilder.Entity<PayPayrollException>().ToTable("payroll_exception", "payroll");

            // Workflow
            modelBuilder.Entity<WfWorkflowMaster>().ToTable("workflow_master", "workflow");
            modelBuilder.Entity<WfWorkflowStep>().ToTable("workflow_step", "workflow");
            modelBuilder.Entity<WfWorkflowTransaction>().ToTable("workflow_transaction", "workflow");
            modelBuilder.Entity<WfApprovalHistory>().ToTable("approval_history", "workflow");
            modelBuilder.Entity<WfPendingApproval>().ToTable("pending_approval", "workflow");
            modelBuilder.Entity<WfDelegation>().ToTable("delegation", "workflow");
            modelBuilder.Entity<WfEscalationRule>().ToTable("escalation_rule", "workflow");
            modelBuilder.Entity<WfNotificationQueue>().ToTable("notification_queue", "workflow");
            modelBuilder.Entity<WfWorkflowAttachment>().ToTable("workflow_attachment", "workflow");
            modelBuilder.Entity<WfWorkflowAudit>().ToTable("workflow_audit", "workflow");

            // Audit
            modelBuilder.Entity<AudLoginHistory>().ToTable("login_history", "audit");
            modelBuilder.Entity<AudDataChange>().ToTable("data_change", "audit");
            modelBuilder.Entity<AudApprovalTrail>().ToTable("approval_trail", "audit");
            modelBuilder.Entity<AudReportAccess>().ToTable("report_access", "audit");
            modelBuilder.Entity<AudExportHistory>().ToTable("export_history", "audit");
            modelBuilder.Entity<AudSystemEvent>().ToTable("system_event", "audit");

            // Recruitment
            modelBuilder.Entity<RecManpowerRequisition>().ToTable("manpower_requisition", "recruitment");
            modelBuilder.Entity<RecJobPosition>().ToTable("job_position", "recruitment");
            modelBuilder.Entity<RecCandidate>().ToTable("candidate", "recruitment");
            modelBuilder.Entity<RecCandidateDocument>().ToTable("candidate_document", "recruitment");
            modelBuilder.Entity<RecInterviewSchedule>().ToTable("interview_schedule", "recruitment");
            modelBuilder.Entity<RecInterviewEvaluation>().ToTable("interview_evaluation", "recruitment");
            modelBuilder.Entity<RecInterviewPanel>().ToTable("interview_panel", "recruitment");
            modelBuilder.Entity<RecOfferLetter>().ToTable("offer_letter", "recruitment");
            modelBuilder.Entity<RecAppointment>().ToTable("appointment", "recruitment");
            modelBuilder.Entity<RecRecruitmentWorkflow>().ToTable("recruitment_workflow", "recruitment");
            //modelBuilder.Entity<Menu>().ToTable("Menus");
            //modelBuilder.Entity<MenuPermission>().ToTable("MenuPermissions");
            //modelBuilder.Entity<ApplicationTracking>().ToTable("ApplicationTrackings");
            //modelBuilder.Entity<OfferLetter>().ToTable("OfferLetters");

            //lookup
            modelBuilder.Entity<LookDivision>().ToTable("division", "lookup");
            modelBuilder.Entity<LookDistrict>().ToTable("district", "lookup");
            modelBuilder.Entity<LookThana>().ToTable("thana", "lookup");
            modelBuilder.Entity<LookBanking>().ToTable("banking", "lookup");
            modelBuilder.Entity<LookEmployeeNature>().ToTable("employee_nature", "lookup");
            //modelBuilder.Entity<LookShift>().ToTable("shifts", "lookup");
            //modelBuilder.Entity<LookHoliday>().ToTable("holidays", "lookup");


            // Apply to all entities that have CreatedOn and UpdatedOn properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configure CreatedOn
                var createdOnProperty = entityType.FindProperty("CreatedOn");
                if (createdOnProperty != null && createdOnProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedOn")
                        .HasDefaultValueSql("NOW()");
                }

                // Configure UpdatedOn
                var updatedOnProperty = entityType.FindProperty("UpdatedOn");
                if (updatedOnProperty != null && updatedOnProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("UpdatedOn")
                        .HasDefaultValueSql("NOW()");
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
