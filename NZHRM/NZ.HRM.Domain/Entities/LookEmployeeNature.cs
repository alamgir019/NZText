using NZ.HRM.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZ.HRM.Domain.Entities;

[Table("employee_nature", Schema = "lookup")]
public class LookEmployeeNature : BaseEntityWithSortOrder
{
    public string NatureName { get; set; } = string.Empty;
}
