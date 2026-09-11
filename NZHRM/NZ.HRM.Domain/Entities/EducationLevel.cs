using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities
{
    [Table("education_level", Schema = "lookup")]
    public class EducationLevel : BaseEntityWithSortOrder
    {
        public string EducationCode { get; set; } = string.Empty;
        public string EducationName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
