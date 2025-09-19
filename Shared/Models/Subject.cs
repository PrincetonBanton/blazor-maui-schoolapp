namespace SchoolApp.Shared.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? GradeLevelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Component> Components { get; set; } = new();
    public bool IsValidated { get; set; } = false;
}
