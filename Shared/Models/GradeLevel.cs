namespace SchoolApp.Shared.Models;

public class GradeLevel
{
    public Guid Id { get; set; }
    public string LevelName { get; set; } = string.Empty; 

    public Guid SchoolYearId { get; set; }
    public SchoolYear? SchoolYear { get; set; }
}

