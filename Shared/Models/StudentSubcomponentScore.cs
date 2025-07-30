namespace SchoolApp.Shared.Models;

public class StudentSubcomponentScore
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StudentId { get; set; }
    public Guid SubcomponentId { get; set; } 
    public int GradingPeriod { get; set; } 
    public double ScoreObtained { get; set; } 
    public DateTime DateRecorded { get; set; } = DateTime.Now;

    // Navigation properties (optional but good for EF Core relations)
    // public Student? Student { get; set; }
    // public Subcomponent? Subcomponent { get; set; }
}