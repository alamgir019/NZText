using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities;

public class EmployeeNature : BaseEntityWithSortOrder
{
    public string NatureName { get; set; } = string.Empty;
}
