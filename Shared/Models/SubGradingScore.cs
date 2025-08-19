namespace SchoolApp.Shared.Models;

public class SubGradingScore
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StudentId { get; set; }
    public Guid SubcomponentId { get; set; }
    public Guid SubGradingItemId { get; set; }
    public int GradingPeriod { get; set; }
    public double ScoreObtained { get; set; }
    public double PercentSubject { get; set; }
    public DateTime DateRecorded { get; set; } = DateTime.Now;
}