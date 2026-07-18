namespace NZ.HRM.Application.Designations.Queries.GetAllDesignations;

public class GetAllDesignationsQuery
{
    public bool IncludeInactive { get; set; } = false;
}

public class DesignationDto
{
    public string Id { get; set; } = string.Empty;
    public string DesignationName { get; set; } = string.Empty;
    public string? DesignationCode { get; set; }
    public string? ParentId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? DesignationNameBangla { get; internal set; }
}
