using System.ComponentModel;

namespace SchoolApp.Shared.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    // Navigation
    public List<Component> Components { get; set; } = new();

    public bool IsValidated { get; set; } = false;
}
