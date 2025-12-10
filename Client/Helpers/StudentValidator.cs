using SchoolApp.Shared.Models;

namespace SchoolApp.Client.Helpers
{
    public static class StudentValidator
    {
        public static string? Validate(Student student)
        {
            if (student == null)
                return "Invalid student data.";

            if (string.IsNullOrWhiteSpace(student.FirstName))
                return "First name is required.";

            if (string.IsNullOrWhiteSpace(student.LastName))
                return "Last name is required.";

            if (student.DateOfBirth == default)
                return "Date of birth is required.";

            if (string.IsNullOrWhiteSpace(student.Gender))
                return "Gender is required.";

            if (string.IsNullOrWhiteSpace(student.Address))
                return "Address is required.";

            if (string.IsNullOrWhiteSpace(student.ContactNumber))
                return "Contact number is required.";

            if (student.GradeLevelId == null)
                return "A grade level must be selected.";

            if (student.SchoolSectionId == null)
                return "A school section must be selected.";

            return null; // no error
        }
    }
}
