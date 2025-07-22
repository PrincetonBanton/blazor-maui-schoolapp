namespace SchoolApp.Shared.Models;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    // Possibly: GradeLevel, Section, etc.
}
