using System;
using NZ.Attendance.Domain.Entities;
using NZ.Attendance.Domain.Enums;

namespace NZ.Attendance.Domain.Services
{
    /// <summary>
    /// Owns all state transitions for <see cref="AttAttendanceException"/>.
    /// Guarantees an audit row is written for every status change.
    /// </summary>
    public class AttendanceExceptionWorkflow
    {
        public void Submit(AttAttendanceException entity, string userId, string? comments = null)
            => Transition(entity, AttendanceExceptionStatus.Submitted, userId, comments);

        public void Approve(AttAttendanceException entity, string reviewerId, string? comments = null)
            => Transition(entity, AttendanceExceptionStatus.Approved, reviewerId, comments);

        public void Reject(AttAttendanceException entity, string reviewerId, string comments)
        {
            if (string.IsNullOrWhiteSpace(comments))
                throw new ArgumentException("Rejection remarks are required.", nameof(comments));

            Transition(entity, AttendanceExceptionStatus.Rejected, reviewerId, comments);
        }

        public void Cancel(AttAttendanceException entity, string userId, string? comments = null)
            => Transition(entity, AttendanceExceptionStatus.Cancelled, userId, comments);

        public void EnsureEditable(AttAttendanceException entity)
        {
            if (entity.Status is not (AttendanceExceptionStatus.Draft or AttendanceExceptionStatus.Rejected))
                throw new InvalidOperationException(
                    $"An attendance exception in '{entity.Status}' state cannot be modified.");
        }

        private void Transition(
            AttAttendanceException entity,
            AttendanceExceptionStatus to,
            string userId,
            string? comments)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User id is required.", nameof(userId));

            if (!IsAllowed(entity.Status, to))
                throw new InvalidOperationException(
                    $"Cannot move an attendance exception from '{entity.Status}' to '{to}'.");

            entity.History.Add(new AttAttendanceExceptionHistory
            {
                AttendanceExceptionId = entity.Id,
                FromStatus = entity.Status,
                ToStatus = to,
                ActionBy = userId,
                ActionOn = DateTime.UtcNow,
                Comments = comments,
                CreatedBy = userId,
                UpdatedBy = userId
            });

            entity.Status = to;
            entity.UpdatedBy = userId;
            entity.UpdatedOn = DateTime.UtcNow;
        }

        private static bool IsAllowed(AttendanceExceptionStatus from, AttendanceExceptionStatus to)
            => (from, to) switch
            {
                (AttendanceExceptionStatus.Draft, AttendanceExceptionStatus.Submitted) => true,
                (AttendanceExceptionStatus.Rejected, AttendanceExceptionStatus.Submitted) => true,
                (AttendanceExceptionStatus.Submitted, AttendanceExceptionStatus.Approved) => true,
                (AttendanceExceptionStatus.Submitted, AttendanceExceptionStatus.Rejected) => true,
                (AttendanceExceptionStatus.Submitted, AttendanceExceptionStatus.Cancelled) => true,
                (AttendanceExceptionStatus.Draft, AttendanceExceptionStatus.Cancelled) => true,
                _ => false
            };
    }
}
