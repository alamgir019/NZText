using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("document_type", Schema = "lookup")]
    public class DocumentType : BaseEntityWithSortOrder
    {
        public string DocumentCode { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public bool MandatoryFlag { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
