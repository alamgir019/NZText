using NZ.HRM.Domain.Services;

namespace NZ.HRM.Infrastructure.Services;

/// <summary>
/// Derives the current business date from the server date.
/// </summary>
public class ServerBusinessCalendar : IBusinessCalendar
{
    public DateOnly CurrentBusinessDate => DateOnly.FromDateTime(DateTime.Now);
}
