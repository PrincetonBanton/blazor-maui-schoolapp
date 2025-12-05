using System.Security.Cryptography;

namespace SchoolApp.Client.Helpers
{
    public static class PasswordHelper
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);

            var hash = Convert.ToBase64String(pbkdf2.GetBytes(32));
            var salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }

        public static bool Verify(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);

            var computedHash = Convert.ToBase64String(pbkdf2.GetBytes(32));

            return computedHash == savedHash;
        }
    }
}
