using System.Globalization;
using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.Attendance.Application.OvertimeRequests.Commands.SubmitOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Dto;
using NZ.Attendance.Application.OvertimeRequests.Commands.CreateOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Commands.AddOvertimeEmployee;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeRequest;
using NZ.Attendance.Application.OvertimeRequests.Commands.ApproveOvertimeEmployee;

namespace NZ.Attendance.Application.OvertimeRequests.Handlers
{
    public class OvertimeRequestCommandHandler
    {
        private readonly IOvertimeRequestRepository _overtimeRequestRepository;
        private readonly IEmployeeMasterRepository _employeeMasterRepository;

        public OvertimeRequestCommandHandler(
            IOvertimeRequestRepository overtimeRequestRepository,
            IEmployeeMasterRepository employeeMasterRepository)
        {
            _overtimeRequestRepository = overtimeRequestRepository;
            _employeeMasterRepository = employeeMasterRepository;
        }

        public async Task<string> Handle(CreateOvertimeRequestCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Employees == null || !command.Employees.Any())
                throw new ArgumentException("OT employee list must contain at least one employee");

            // Validate and map employees
            var seen = new HashSet<string>();
            var dto = new OvertimeRequestDto
            {
                CurrentShiftId = command.CurrentShiftId,
                OTDate = command.OTDate,
                DepartmentId = command.DepartmentId,
                Reason = command.Reason
            };

            foreach (var emp in command.Employees)
            {
                if (string.IsNullOrWhiteSpace(emp.EmployeeId))
                    throw new ArgumentException("EmployeeId is required for each OT employee");

                if (!seen.Add(emp.EmployeeId))
                    throw new ArgumentException($"Duplicate employee {emp.EmployeeId} in OT request");

                var exists = await _employeeMasterRepository.ExistsAsync(emp.EmployeeId, cancellationToken);
                if (!exists)
                    throw new KeyNotFoundException($"Employee {emp.EmployeeId} not found");

                if (!TimeSpan.TryParseExact(emp.OTHours, @"hh\:mm", CultureInfo.InvariantCulture, out var ts))
                    throw new FormatException($"Invalid OT hours format for employee {emp.EmployeeId}");

                if (ts <= TimeSpan.Zero)
                    throw new ArgumentException($"OT hours must be greater than 00:00 for employee {emp.EmployeeId}");

                dto.Employees.Add(new OvertimeEmployeeDto
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeCode = emp.EmployeeCode,
                    EmployeeName = emp.EmployeeName,
                    OTHours = emp.OTHours
                });
            }

            var id = await _overtimeRequestRepository.CreateAsync(dto);
            return id;
        }

        public async Task Handle(List<ApproveOvertimeRequestCommand> commands, CancellationToken cancellationToken = default)
        {
            if (commands == null || !commands.Any())
                throw new ArgumentException("No commands provided");
            await _overtimeRequestRepository.ApproveAsync(commands, cancellationToken);
        }
    }
}
