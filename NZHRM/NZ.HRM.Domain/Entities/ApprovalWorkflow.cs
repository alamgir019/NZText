namespace NZ.HRM.Domain.Entities
{
    public class ApprovalWorkflow
    {
        public int WorkflowId { get; set; }
        public string Module { get; set; } = string.Empty;
        public int Level { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
