namespace SchoolApp.Shared.Models;

public class SubjectGradingItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubjectId { get; set; }
    public Guid SubcomponentId { get; set; }
    public int GradingPeriod { get; set; } // 1 = 1st, 2 = 2nd, etc.
    public int ItemCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
