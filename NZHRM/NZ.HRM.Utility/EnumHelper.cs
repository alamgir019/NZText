namespace NZ.HRM.Utility
{
    public static class EnumHelper
    {
        /// <summary>
        /// Safely parses a string to an enum value with null handling.
        /// </summary>
        /// <typeparam name="T">The enum type to parse to</typeparam>
        /// <param name="value">The string value to parse</param>
        /// <returns>The parsed enum value, or null if parsing fails or value is null</returns>
        public static T? TryParseEnum<T>(string? value) where T : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (System.Enum.TryParse<T>(value, out var result))
                return result;

            return null;
        }
    }
}
