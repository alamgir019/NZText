using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NZ.Attendance.Application.AttendanceExceptions.Commands.CreateAttendanceExceptions;
using NZ.Attendance.Application.AttendanceExceptions.Commands.DeleteAttendanceException;
using NZ.Attendance.Application.AttendanceExceptions.Commands.ReviewAttendanceException;
using NZ.Attendance.Application.AttendanceExceptions.Commands.UpdateAttendanceException;
using NZ.Attendance.Application.Interfaces.Repositories;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.Attendance.Application.AttendanceExceptions.Handlers
{
    public class AttendanceExceptionCommandHandler
    {
        private readonly IAttendanceExceptionRepository _repository;
        private readonly IEmployeeMasterRepository _employeeMasterRepository;

        public AttendanceExceptionCommandHandler(
            IAttendanceExceptionRepository repository,
            IEmployeeMasterRepository employeeMasterRepository)
        {
            _repository = repository;
            _employeeMasterRepository = employeeMasterRepository;
        }

        public async Task<List<string>> Handle(
            CreateAttendanceExceptionsCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.Items == null || command.Items.Count == 0)
                throw new ArgumentException("At least one attendance exception is required");

            if (string.IsNullOrWhiteSpace(command.UserId))
                throw new ArgumentException("UserId is required");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var seen = new HashSet<(string, DateOnly)>();

            foreach (var item in command.Items)
            {
                if (string.IsNullOrWhiteSpace(item.EmployeeId))
                    throw new ArgumentException("EmployeeId is required for each attendance exception");

                if (string.IsNullOrWhiteSpace(item.ExceptionType))
                    throw new ArgumentException($"ExceptionType is required for employee {item.EmployeeId}");

                if (item.AttendanceDate > today)
                    throw new ArgumentException($"Attendance date cannot be in the future for employee {item.EmployeeId}");

                if (!seen.Add((item.EmployeeId, item.AttendanceDate)))
                    throw new ArgumentException(
                        $"Duplicate employee {item.EmployeeId} on {item.AttendanceDate:yyyy-MM-dd} in the request");

                var exists = await _employeeMasterRepository.ExistsAsync(item.EmployeeId, cancellationToken);
                if (!exists)
                    throw new KeyNotFoundException($"Employee {item.EmployeeId} not found");
            }

            var existingKeys = await _repository.GetExistingKeysAsync(
                command.Items.Select(i => i.EmployeeId).Distinct(),
                command.Items.Select(i => i.AttendanceDate).Distinct(),
                cancellationToken);

            var clash = command.Items
                .FirstOrDefault(i => existingKeys.Contains((i.EmployeeId, i.AttendanceDate)));

            if (clash != null)
                throw new InvalidOperationException(
                    $"An attendance exception already exists for employee {clash.EmployeeId} on {clash.AttendanceDate:yyyy-MM-dd}");

            return await _repository.CreateRangeAsync(command, cancellationToken);
        }

        public async Task Handle(
            UpdateAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
                throw new ArgumentException("Id is required");

            if (string.IsNullOrWhiteSpace(command.ExceptionType))
                throw new ArgumentException("ExceptionType is required");

            await _repository.UpdateAsync(
                command.Id, command.ExceptionType, command.Severity, command.Remarks,
                command.UserId, cancellationToken);
        }

        public async Task Handle(
            DeleteAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
                throw new ArgumentException("Id is required");

            await _repository.DeleteAsync(command.Id, command.UserId, cancellationToken);
        }

        public async Task Handle(
            SubmitAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
            => await _repository.SubmitAsync(command.Id, command.UserId, command.Comments, cancellationToken);

        public async Task Handle(
            ApproveAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
            => await _repository.ApproveAsync(command.Id, command.UserId, command.Comments, cancellationToken);

        public async Task Handle(
            RejectAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Comments))
                throw new ArgumentException("Rejection remarks are required");

            await _repository.RejectAsync(command.Id, command.UserId, command.Comments, cancellationToken);
        }

        public async Task Handle(
            CancelAttendanceExceptionCommand command,
            CancellationToken cancellationToken = default)
            => await _repository.CancelAsync(command.Id, command.UserId, command.Comments, cancellationToken);
    }
}
