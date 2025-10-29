namespace SchoolApp.Shared.Models;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? SchoolSectionId { get; set; }
    public Guid? GradeLevelId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {(string.IsNullOrWhiteSpace(MiddleName) ? "" : MiddleName + " ")}{LastName}".Trim();

    public DateTime DateOfBirth { get; set; } = DateTime.Today;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;

    // Navigation (optional but useful for joins)
    //public SchoolSection? SchoolSection { get; set; }
    //public SchoolYear? SchoolYear { get; set; }
}
