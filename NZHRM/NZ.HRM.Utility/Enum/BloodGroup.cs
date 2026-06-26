using System.Runtime.Serialization;

namespace NZ.HRM.Utility.Enum
{
    public enum BloodGroup
    {
        [EnumMember(Value = "A+")]
        APositive,    // A+
        [EnumMember(Value = "A-")]
        ANegative,    // A-
        [EnumMember(Value = "B+")]
        BPositive,    // B+
        [EnumMember(Value = "B-")]
        BNegative,    // B-
        [EnumMember(Value = "O+")]
        OPositive,    // O+
        [EnumMember(Value = "O-")]
        ONegative,    // O-
        [EnumMember(Value = "AB+")]
        ABPositive,   // AB+
        [EnumMember(Value = "AB-")]
        ABNegative    // AB-
    }
}
