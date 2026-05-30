namespace NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessHistoryByEmployeeId;

public class MedicalFitnessHistoryDto
{
    public string MedicalFitnessCheckId { get; set; } = string.Empty;
    public DateTime ExaminationDateTime { get; set; }
    public string ExaminedByDoctor { get; set; } = string.Empty;
    public bool IsFit { get; set; }
}
