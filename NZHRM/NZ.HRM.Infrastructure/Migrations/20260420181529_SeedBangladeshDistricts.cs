using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBangladeshDistricts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "DistrictName", "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy", "IsActive" },
                values: new object[,]
                {
                    // Dhaka Division (13 districts)
                    { "01HQZXY00000000000000001", "Dhaka", now, "System", now, "System", true },
                    { "01HQZXY00000000000000002", "Faridpur", now, "System", now, "System", true },
                    { "01HQZXY00000000000000003", "Gazipur", now, "System", now, "System", true },
                    { "01HQZXY00000000000000004", "Gopalganj", now, "System", now, "System", true },
                    { "01HQZXY00000000000000005", "Kishoreganj", now, "System", now, "System", true },
                    { "01HQZXY00000000000000006", "Madaripur", now, "System", now, "System", true },
                    { "01HQZXY00000000000000007", "Manikganj", now, "System", now, "System", true },
                    { "01HQZXY00000000000000008", "Munshiganj", now, "System", now, "System", true },
                    { "01HQZXY00000000000000009", "Narayanganj", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000A", "Narsingdi", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000B", "Rajbari", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000C", "Shariatpur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000D", "Tangail", now, "System", now, "System", true },

                    // Chittagong Division (11 districts)
                    { "01HQZXY0000000000000000E", "Bandarban", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000F", "Brahmanbaria", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000G", "Chandpur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000H", "Chittagong", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000I", "Comilla", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000J", "Cox's Bazar", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000K", "Feni", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000L", "Khagrachari", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000M", "Lakshmipur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000N", "Noakhali", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000O", "Rangamati", now, "System", now, "System", true },

                    // Rajshahi Division (8 districts)
                    { "01HQZXY0000000000000000P", "Bogra", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000Q", "Joypurhat", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000R", "Naogaon", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000S", "Natore", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000T", "Chapainawabganj", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000U", "Pabna", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000V", "Rajshahi", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000W", "Sirajganj", now, "System", now, "System", true },

                    // Khulna Division (10 districts)
                    { "01HQZXY0000000000000000X", "Bagerhat", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000Y", "Chuadanga", now, "System", now, "System", true },
                    { "01HQZXY0000000000000000Z", "Jessore", now, "System", now, "System", true },
                    { "01HQZXY00000000000000010", "Jhenaidah", now, "System", now, "System", true },
                    { "01HQZXY00000000000000011", "Khulna", now, "System", now, "System", true },
                    { "01HQZXY00000000000000012", "Kushtia", now, "System", now, "System", true },
                    { "01HQZXY00000000000000013", "Magura", now, "System", now, "System", true },
                    { "01HQZXY00000000000000014", "Meherpur", now, "System", now, "System", true },
                    { "01HQZXY00000000000000015", "Narail", now, "System", now, "System", true },
                    { "01HQZXY00000000000000016", "Satkhira", now, "System", now, "System", true },

                    // Barisal Division (6 districts)
                    { "01HQZXY00000000000000017", "Barguna", now, "System", now, "System", true },
                    { "01HQZXY00000000000000018", "Barisal", now, "System", now, "System", true },
                    { "01HQZXY00000000000000019", "Bhola", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001A", "Jhalokati", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001B", "Patuakhali", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001C", "Pirojpur", now, "System", now, "System", true },

                    // Sylhet Division (4 districts)
                    { "01HQZXY0000000000000001D", "Habiganj", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001E", "Moulvibazar", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001F", "Sunamganj", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001G", "Sylhet", now, "System", now, "System", true },

                    // Rangpur Division (8 districts)
                    { "01HQZXY0000000000000001H", "Dinajpur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001I", "Gaibandha", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001J", "Kurigram", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001K", "Lalmonirhat", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001L", "Nilphamari", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001M", "Panchagarh", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001N", "Rangpur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001O", "Thakurgaon", now, "System", now, "System", true },

                    // Mymensingh Division (4 districts)
                    { "01HQZXY0000000000000001P", "Jamalpur", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001Q", "Mymensingh", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001R", "Netrokona", now, "System", now, "System", true },
                    { "01HQZXY0000000000000001S", "Sherpur", now, "System", now, "System", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    "01HQZXY00000000000000001", "01HQZXY00000000000000002", "01HQZXY00000000000000003", "01HQZXY00000000000000004",
                    "01HQZXY00000000000000005", "01HQZXY00000000000000006", "01HQZXY00000000000000007", "01HQZXY00000000000000008",
                    "01HQZXY00000000000000009", "01HQZXY0000000000000000A", "01HQZXY0000000000000000B", "01HQZXY0000000000000000C",
                    "01HQZXY0000000000000000D", "01HQZXY0000000000000000E", "01HQZXY0000000000000000F", "01HQZXY0000000000000000G",
                    "01HQZXY0000000000000000H", "01HQZXY0000000000000000I", "01HQZXY0000000000000000J", "01HQZXY0000000000000000K",
                    "01HQZXY0000000000000000L", "01HQZXY0000000000000000M", "01HQZXY0000000000000000N", "01HQZXY0000000000000000O",
                    "01HQZXY0000000000000000P", "01HQZXY0000000000000000Q", "01HQZXY0000000000000000R", "01HQZXY0000000000000000S",
                    "01HQZXY0000000000000000T", "01HQZXY0000000000000000U", "01HQZXY0000000000000000V", "01HQZXY0000000000000000W",
                    "01HQZXY0000000000000000X", "01HQZXY0000000000000000Y", "01HQZXY0000000000000000Z", "01HQZXY00000000000000010",
                    "01HQZXY00000000000000011", "01HQZXY00000000000000012", "01HQZXY00000000000000013", "01HQZXY00000000000000014",
                    "01HQZXY00000000000000015", "01HQZXY00000000000000016", "01HQZXY00000000000000017", "01HQZXY00000000000000018",
                    "01HQZXY00000000000000019", "01HQZXY0000000000000001A", "01HQZXY0000000000000001B", "01HQZXY0000000000000001C",
                    "01HQZXY0000000000000001D", "01HQZXY0000000000000001E", "01HQZXY0000000000000001F", "01HQZXY0000000000000001G",
                    "01HQZXY0000000000000001H", "01HQZXY0000000000000001I", "01HQZXY0000000000000001J", "01HQZXY0000000000000001K",
                    "01HQZXY0000000000000001L", "01HQZXY0000000000000001M", "01HQZXY0000000000000001N", "01HQZXY0000000000000001O",
                    "01HQZXY0000000000000001P", "01HQZXY0000000000000001Q", "01HQZXY0000000000000001R", "01HQZXY0000000000000001S"
                });
        }
    }
}
