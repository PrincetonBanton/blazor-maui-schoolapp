namespace SchoolApp.Shared.Models;

public class GradeEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StudentId { get; set; }
    public Guid SubcomponentId { get; set; }

    public int Items { get; set; }           // Optional override (if variable)
    public int Score { get; set; }           // Actual score student got
}
