namespace ValorVault.Services
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string hashedPassword, string candidatePassword)
        {
            return BCrypt.Net.BCrypt.Verify(candidatePassword, hashedPassword);
        }
    }
}