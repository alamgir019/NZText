using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NZ.Attendance.Domain.Entities;
using NZ.Attendance.Domain.Services;
using NZ.Attendance.Infrastructure.Persistence;
using NZ.HRM.Domain.Entities;

namespace NZ.Attendance.Infrastructure.PunchPolling;

public class PunchPollingBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDevicePunchSource _devicePunchSource;
    private readonly IOptions<PunchPollingOptions> _options;
    private readonly ILogger<PunchPollingBackgroundService> _logger;

    public PunchPollingBackgroundService(
        IServiceScopeFactory scopeFactory,
        IDevicePunchSource devicePunchSource,
        IOptions<PunchPollingOptions> options,
        ILogger<PunchPollingBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _devicePunchSource = devicePunchSource;
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
         while (!stoppingToken.IsCancellationRequested)
        {
            var options = _options.Value;

            if (!options.Enabled)
            {
                await Task.Delay(TimeSpan.FromSeconds(Math.Max(10, 10)), stoppingToken);
                continue;
            }

            try
            {
                var rangeEnd = DateTime.Today;
                var rangeStart = rangeEnd;

                var units = options.Units
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (units.Count == 0)
                {
                    _logger.LogWarning("Punch polling is enabled but no units are configured.");
                    await Task.Delay(TimeSpan.FromSeconds(Math.Max(10, options.PollIntervalSeconds)), stoppingToken);
                    continue;
                }

                foreach (var unit in units)
                {
                    var punches = await _devicePunchSource.PullNewPunchesAsync(unit, rangeStart, rangeEnd, stoppingToken);

                    if (punches.Count > 0)
                    {
                        _logger.LogInformation("Pulled {Count} punch records from device source for unit {Unit}", punches.Count, unit);
                    }

                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();
                    var importBatchId = Guid.NewGuid().ToString("N");

                    var lookupName = string.IsNullOrWhiteSpace(options.DeviceName) ? unit : options.DeviceName;
                    var device = await dbContext.AttDeviceMasters
                        .FirstOrDefaultAsync(d => d.DeviceName == lookupName, stoppingToken);

                    if (device == null)
                    {
                        _logger.LogWarning("Device '{DeviceName}' not found in device_master table. Skipping unit {Unit}.", lookupName, unit);
                        continue;
                    }

                    var deviceId = device.Id;

                    var syncLog = new AttDeviceSyncLog
                    {
                        DeviceId = deviceId,
                        SyncStartTime = DateTime.UtcNow,
                        SyncStatus = $"Running:{unit}",
                        PunchCount = 0
                    };

                    dbContext.AttDeviceSyncLogs.Add(syncLog);
                    await dbContext.SaveChangesAsync(stoppingToken);

                    var incomingKeys = punches
                        .Select(p => new
                        {
                            EmployeeCode = p.EmployeeCode,
                            PunchDate = p.PunchDate,
                            PunchTime = p.PunchTime,
                            DeviceId = p.DeviceId ?? deviceId
                        })
                        .ToHashSet();

                    var employeeCodes = incomingKeys.Select(k => k.EmployeeCode).Distinct().ToList();
                    var punchDates = incomingKeys.Select(k => k.PunchDate).Distinct().ToList();

                    var existingKeys = await dbContext.AttRawPunches
                        .Where(x => employeeCodes.Contains(x.EmployeeCode!)
                                    && punchDates.Contains(x.PunchDate)
                                    && x.DeviceId == deviceId)
                        .Select(x => new { x.EmployeeCode, x.PunchDate, x.PunchTime })
                        .ToHashSetAsync(stoppingToken);

                    var newPunches = punches
                        .Where(p => !existingKeys.Contains(
                            new {
                            EmployeeCode = p.EmployeeCode,
                            PunchDate = p.PunchDate,
                            PunchTime = p.PunchTime,
                        }))
                        .Select(punch => new AttRawPunch
                        {
                            EmployeeCode = punch.EmployeeCode,
                            PunchDate = punch.PunchDate,
                            PunchTime = punch.PunchTime,
                            DeviceId = deviceId,
                            DeviceLocation = punch.DeviceLocation ?? unit,
                            VerificationMode = punch.VerificationMode ?? options.VerificationMode,
                            PunchSource = punch.PunchSource ?? $"{options.PunchSource}:{unit}",
                            ImportBatchId = importBatchId,
                            PunchStatus = "Imported",
                            CreatedDate = DateTime.UtcNow,
                            PunchType = punch.PunchType
                        })
                        .ToList();

                    var newEmployeeCodes = newPunches.Select(p => p.EmployeeCode).Distinct().ToList();
                    var employeeIdMap = await dbContext.HrmEmployeeMasters
                        .Where(e => newEmployeeCodes.Contains(e.EmployeeCode))
                        .Select(e => new { e.EmployeeCode, e.Id })
                        .ToDictionaryAsync(e => e.EmployeeCode, e => e.Id, stoppingToken);
                    newPunches.ForEach(p => p.EmployeeId = p.EmployeeCode != null && employeeIdMap.TryGetValue(p.EmployeeCode, out var id) ? id : null);
                    await dbContext.AttRawPunches.AddRangeAsync(newPunches, stoppingToken);
                    var insertedCount = newPunches.Count;

                    syncLog.PunchCount = insertedCount;
                    syncLog.SyncStatus = $"Success:{unit}";
                    syncLog.SyncEndTime = DateTime.UtcNow;
                    await dbContext.SaveChangesAsync(stoppingToken);

                    await ProcessAttendanceAsync(dbContext, DateOnly.FromDateTime(rangeStart), DateOnly.FromDateTime(rangeEnd), employeeIdMap, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();
                    dbContext.AttDeviceSyncLogs.Add(new AttDeviceSyncLog
                    {
                        DeviceId = _options.Value.DeviceName,
                        SyncStartTime = DateTime.UtcNow,
                        SyncEndTime = DateTime.UtcNow,
                        SyncStatus = "Failed",
                        PunchCount = 0,
                        ErrorMessage = ex.Message
                    });
                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                catch (Exception logEx)
                {
                    _logger.LogError(logEx, "Failed to persist punch sync error log");
                }

                _logger.LogError(ex, "Error occurred while polling and processing punches");
            }

            await Task.Delay(TimeSpan.FromSeconds(Math.Max(10, _options.Value.PollIntervalSeconds)), stoppingToken);
        }
    }

    private async Task ProcessAttendanceAsync(
        AttendanceDbContext dbContext,
        DateOnly from,
        DateOnly to,
        Dictionary<string, string> employeeIdMap,
        CancellationToken cancellationToken)
    {
        if (employeeIdMap.Count == 0) return;

        var employeeIds = employeeIdMap.Values.Where(id => id != null).Distinct().ToList();
        var dateRange = Enumerable.Range(0, to.DayNumber - from.DayNumber + 1)
                            .Select(d => from.AddDays(d))
                            .ToList();

        var rawPunches = await dbContext.AttRawPunches
            .Where(p => employeeIds.Contains(p.EmployeeId!)
                     && p.PunchDate >= from
                     && p.PunchDate <= to)
            .Select(p => new { p.EmployeeId, p.PunchDate, p.PunchTime })
            .ToListAsync(cancellationToken);

        var holidayDates = await dbContext.LevHolidayCalendars
            .Where(h => h.HolidayDate >= from && h.HolidayDate <= to && h.Status)
            .Select(h => h.HolidayDate)
            .ToHashSetAsync(cancellationToken);

        var weeklyOffRaw = await dbContext.AttWeeklyOffPatterns
            .Where(w => employeeIds.Contains(w.EmployeeId)
                     && w.Status
                     && (w.EffectiveDate == null || w.EffectiveDate <= to))
            .Select(w => new { w.EmployeeId, w.DayOfWeek })
            .ToListAsync(cancellationToken);

        var weeklyOffMap = weeklyOffRaw
            .GroupBy(w => w.EmployeeId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(w => Enum.Parse<DayOfWeek>(w.DayOfWeek, true))
                       .ToHashSet());

        var shiftRosters = await dbContext.AttShiftRosters
            .Where(r => employeeIds.Contains(r.EmployeeId)
                     && r.RosterDate >= from
                     && r.RosterDate <= to
                     && r.Status)
            .Select(r => new { r.EmployeeId, r.RosterDate, r.ShiftId })
            .ToListAsync(cancellationToken);

        var existing = await dbContext.AttProcessedAttendances
            .Where(a => employeeIds.Contains(a.EmployeeId)
                     && a.AttendanceDate >= from
                     && a.AttendanceDate <= to)
            .ToListAsync(cancellationToken);

        dbContext.AttProcessedAttendances.RemoveRange(existing);

        var processor = new AttendanceProcessingService();
        var allRecords = new List<AttProcessedAttendance>();

        foreach (var employeeId in employeeIds)
        {
            var empPunches = rawPunches
                .Where(p => p.EmployeeId == employeeId)
                .Select(p => (p.PunchDate, p.PunchTime));

            var shiftByDate = shiftRosters
                .Where(r => r.EmployeeId == employeeId)
                .ToDictionary(r => r.RosterDate, r => r.ShiftId);

            // If no roster is available for this employee, try to use the employee's assigned shift
            if (shiftByDate == null || shiftByDate.Count == 0)
            {
                var empEmployment = await dbContext.Set< HrmEmployeeEmployment >()
                    .Where(e => e.EmployeeId == employeeId)
                    .Select(e => e.ShiftId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(empEmployment))
                {
                    shiftByDate = new Dictionary<DateOnly, string> { { dateRange[0], empEmployment! } };
                }
                else
                {
                    // Fallback: pick any configured shift from master if employee has no assigned shift
                    var anyShift = await dbContext.MstShifts.Select(s => s.Id).Take(1).FirstOrDefaultAsync(cancellationToken);
                    if (!string.IsNullOrWhiteSpace(anyShift))
                    {
                        shiftByDate = new Dictionary<DateOnly, string> { { dateRange[0], anyShift } };
                    }
                    else
                    {
                        // Last resort: keep dictionary empty; processor may handle missing shift
                        shiftByDate = new Dictionary<DateOnly, string>();
                    }
                }
            }

            var records = processor.Process(
                employeeId,
                dateRange,
                empPunches,
                holidayDates,
                shiftByDate,
                weeklyOffMap.ContainsKey(employeeId) ? weeklyOffMap[employeeId] : new HashSet<DayOfWeek>());

            allRecords.AddRange(records);
        }

        await dbContext.AttProcessedAttendances.AddRangeAsync(allRecords, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Processed attendance for {EmployeeCount} employees covering {DateCount} days ({From} – {To}). Total records: {Count}",
            employeeIds.Count, dateRange.Count, from, to, allRecords.Count);
    }
}
