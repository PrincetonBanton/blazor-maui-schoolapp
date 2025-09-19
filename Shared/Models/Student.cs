namespace SchoolApp.Shared.Models;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? SchoolSectionId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } 
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {(string.IsNullOrWhiteSpace(MiddleName) ? "" : MiddleName + " ")}{LastName}".Trim();
                                         
    public DateTime DateOfBirth { get; set; } = DateTime.Today; 
    public string Gender { get; set; } = string.Empty; 
    public string Address { get; set; } = string.Empty; 
    public string ContactNumber { get; set; } = string.Empty;

    // public string ParentGuardianName { get; set; } = string.Empty;
    // public string ParentGuardianContact { get; set; } = string.Empty;
    // public string ParentGuardianEmail { get; set; } = string.Empty;

    // --- Other potentially useful fields (future consideration) ---
    // public string ProfilePictureUrl { get; set; } = string.Empty;
}