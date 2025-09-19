using SchoolApp.Shared.Models;

namespace SchoolApp.Shared.Services
{
    public class SchoolContextService
    {
        public Guid? SelectedSchoolYearId { get; set; }
        public Guid? SelectedGradeLevelId { get; set; }
        public Guid? SelectedSchoolSectionId { get; set; }

        private List<SchoolYear> SchoolYears { get; set; } = new();
        private List<GradeLevel> GradeLevels { get; set; } = new();
        private List<SchoolSection> SchoolSections { get; set; } = new();

        public event Action? OnContextChanged;

        public void SetSchoolYears(List<SchoolYear> years) => SchoolYears = years;
        public void SetGradeLevels(List<GradeLevel> levels) => GradeLevels = levels;
        public void SetSchoolSections(List<SchoolSection> sections) => SchoolSections = sections;

        public string GetSelectedSchoolYearName()
        {
            var sy = SchoolYears.FirstOrDefault(y => y.Id == SelectedSchoolYearId);
            return sy?.SchoolYearName ?? "No school year selected";
        }

        public string GetSelectedGradeLevelName()
        {
            var gl = GradeLevels.FirstOrDefault(s => s.Id == SelectedGradeLevelId);
            return gl?.LevelName ?? "No grade level selected";
        }

        public string GetSelectedSchoolSectionName()
        {
            var ss = SchoolSections.FirstOrDefault(s => s.Id == SelectedSchoolSectionId);
            return ss?.SectionName ?? "No grade level selected";
        }

        public event Action? OnShowSchoolYearModal;
        public event Action? OnShowGradeLevelModal;
        public event Action? OnShowSchoolSectionModal;
        public void TriggerShowSchoolYearModal() => OnShowSchoolYearModal?.Invoke();
        public void TriggerShowGradeLevelModal() => OnShowGradeLevelModal?.Invoke();
        public void TriggerShowSchoolSectionModal() => OnShowSchoolSectionModal?.Invoke();
    }

}
