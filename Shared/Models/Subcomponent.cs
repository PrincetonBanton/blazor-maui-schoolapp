namespace SchoolApp.Shared.Models;

public class Subcomponent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ComponentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Percentage { get; set; }

    // Scoring
    public int Items { get; set; }
    public int Score { get; set; }
}
