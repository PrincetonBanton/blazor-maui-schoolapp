namespace SchoolApp.Shared.Models;

public class Component
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Percentage { get; set; }
    public List<Subcomponent> Subcomponents { get; set; } = new();
}
