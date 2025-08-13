namespace SchoolApp.Shared.Models;

public class SubGradingItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubjectId { get; set; }
    public Guid SubcomponentId { get; set; }
    public int GradingPeriod { get; set; } 
    public int ItemCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
