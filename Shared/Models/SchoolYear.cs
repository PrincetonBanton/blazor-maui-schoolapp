namespace SchoolApp.Shared.Models;

public class SchoolYear
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SchoolYearName { get; set; } = string.Empty;
}
