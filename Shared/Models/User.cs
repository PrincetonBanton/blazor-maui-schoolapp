    namespace SchoolApp.Server.Models
    {
        public class User
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Username { get; set; } = "";
            public string Password { get; set; } = ""; // plain text for now
            public string Role { get; set; } = "Teacher";
        }
    }
