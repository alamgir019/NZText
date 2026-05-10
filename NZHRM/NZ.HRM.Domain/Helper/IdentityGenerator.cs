using System.Text;

namespace NZ.HRM.Domain.Helper
{
    /// <summary>
    /// IdentityGenerator.
    /// </summary>
    public static class IdentityGenerator
    {
        /// <summary>
        /// The length of generated id.
        /// </summary>
        public const int LENGTH = 26;

        /// <summary>
        /// IDDBTYPE.
        /// </summary>
        public const string IDDBTYPE = "CHAR(26)";

        private static readonly string ValidChars = "QAZ2WSX3" + "EDC4RFV5" + "TGB6YHN7" + "UJM8K9LP";

        /// <summary>
        /// Next.
        /// </summary>
        /// <returns>string.</returns>
        public static string Next()
        {
            return ToBase32String(Guid.NewGuid().ToByteArray());
        }

        /// <summary>
        /// Tos the base32 string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>A string.</returns>
        private static string ToBase32String(byte[] bytes)
        {
            StringBuilder sb = new(); // holds the base32 chars
            var end = bytes.Length * 8;
            Array.Resize(ref bytes, bytes.Length + 1);
            bytes[^1] = 0;
            int offset = 0;

            while (offset < end)
            {
                var arrayOffset = offset >> 3;
                var byteOffset = offset % 8;
                ushort currentWord = (ushort)((256 * bytes[arrayOffset + 1]) + bytes[arrayOffset]);
                var index = (ushort)((ushort)(currentWord << (11 - byteOffset)) >> 11);
                sb.Append(ValidChars[index]);
                offset += 5;
            }

            return sb.ToString();
        }
    }
}
