using System.Runtime.Serialization;

namespace NZ.HRM.Utility.Enum
{
    public enum Shift
    {
        [EnumMember(Value = "Morning Shift(6:00 AM - 2:00 PM)")]
        MorningShift,
        [EnumMember(Value = "Day Shift(2:00 PM - 10:00 PM)")]
        DayShift,
        [EnumMember(Value = "Night Shift(10:00 PM - 6:00 AM)")]
        NightShift,
        [EnumMember(Value = "Evening Shift(6:00 PM - 2:00 AM)")]    
        EveningShift,
        GeneralShift,
        RotatingShift
    }
}
