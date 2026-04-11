namespace NZ.HRM.Domain.Entities
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public int Level { get; set; }
    }
}
