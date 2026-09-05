namespace NZ.HRM.Domain.Services
{
    /// <summary>
    /// Provides the current business date used by domain rules.
    /// </summary>
    public interface IBusinessCalendar
    {
        DateOnly CurrentBusinessDate { get; }
    }
}
