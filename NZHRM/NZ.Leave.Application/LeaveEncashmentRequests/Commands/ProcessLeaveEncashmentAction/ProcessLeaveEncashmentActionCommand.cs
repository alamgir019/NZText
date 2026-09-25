using System.Text.Json.Serialization;

namespace NZ.Leave.Application.LeaveEncashmentRequests.Commands.ProcessLeaveEncashmentAction
{
    public class ProcessLeaveEncashmentActionCommand
    {
        public string RequestId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string? Remarks { get; set; }

        [JsonIgnore]
        public string? ProcessedBy { get; set; }
    }
}