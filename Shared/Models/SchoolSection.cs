namespace SchoolApp.Shared.Models
{
    public class SchoolSection
    {
        public Guid Id { get; set; }
        public string SectionName { get; set; } = string.Empty;

        public Guid GradeLevelId { get; set; }
        public GradeLevel? GradeLevel { get; set; }
    }
}
