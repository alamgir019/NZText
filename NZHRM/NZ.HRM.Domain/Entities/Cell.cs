using System.ComponentModel.DataAnnotations;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class Cell : BaseEntityWithSortOrder
    {
        [Required]
        [MaxLength(100)]
        public string NameEnglish { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? NameBangla { get; set; }

        public ICollection<SectionCell>? SectionCells { get; set; }
    }
}
