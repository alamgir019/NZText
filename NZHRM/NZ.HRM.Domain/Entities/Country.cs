using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("country", Schema = "lookup")]
    public class Country : BaseEntityWithSortOrder
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string NationalityName { get; set; } = string.Empty;
        public bool ActiveFlag { get; set; } = true;
        public ICollection<Division>? Divisions { get; set; }
    }
}
