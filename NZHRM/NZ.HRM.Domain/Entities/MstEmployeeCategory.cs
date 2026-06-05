using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_employee_category", Schema = "master")]
    public class MstEmployeeCategory : BaseEntityWithSortOrder
    {
        public string CategoryCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public bool OtEligible { get; set; }
    }
}
