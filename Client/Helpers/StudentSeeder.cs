using SchoolApp.Shared.Models;

namespace SchoolApp.Client.Helpers
{
    public static class StudentSeeder
    {
        private static readonly Random rand = new();

        private static readonly string[] firstNames =
        {
            "Juan","Pedro","Maria","Jose","Ana","Liza","Carlos","Rosa","Mark","Ella",
            "John","Paul","Grace","Angel","Leo","Nina","Kevin","Samantha","Daniel","Mia",
            "Allan","Jessa","Noel","Cathy","Ramon","April","Dennis","Jenny","Victor","Lourdes"
        };

        private static readonly string[] lastNames =
        {
            "Santos","Reyes","Cruz","Dela Cruz","Garcia","Torres","Mendoza","Flores","Lopez","Ramos",
            "Gonzales","Rivera","Bautista","Castro","Domingo","Fernandez","Gutierrez","Jimenez","Lim","Manalo",
            "Navarro","Ocampo","Perez","Quinto","Soriano","Tan","Valdez","Yap","Zamora","Villanueva"
        };

        private static readonly string[] addresses =
        {
            "Davao City","Sta. Cruz","Toril","Buhangin","Calinan",
            "Mintal","Matina","Tugbok","Bago Aplaya","Panacan"
        };

        private static readonly string[] genders = { "Male", "Female" };

        public static Student GenerateRandomStudent(Guid? gradeLevelId, Guid? sectionId)
        {
            return new Student
            {
                GradeLevelId = gradeLevelId,
                SchoolSectionId = sectionId,
                FirstName = firstNames[rand.Next(firstNames.Length)],
                MiddleName = "M.",
                LastName = lastNames[rand.Next(lastNames.Length)],
                DateOfBirth = DateTime.Now.AddYears(-7 - rand.Next(2)).AddDays(rand.Next(365)),
                Gender = genders[rand.Next(genders.Length)],
                Address = addresses[rand.Next(addresses.Length)],
                ContactNumber = "09" + rand.Next(100000000, 999999999)
            };
        }
    }
}
