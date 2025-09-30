namespace SchoolApp.Shared.Models
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Guid SchoolSectionId { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SchoolYearId { get; set; }

        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }   // INTEGER in SQLite maps to bool in EF Core
        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
