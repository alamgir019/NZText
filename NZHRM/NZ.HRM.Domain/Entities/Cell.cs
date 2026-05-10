using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Cell : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string NameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? NameBangla { get; set; }

        [Required]
        public string SectionId { get; set; } = string.Empty;

        [ForeignKey(nameof(SectionId))]
        public Section? Section { get; set; }
    }
}
