using System.Text.RegularExpressions;

namespace ValorVault.Services
{
    public class InputValidator
    {
        public static bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 18)
                return false;
            string namePattern = "^[a-zA-Z& ]*$";

            return Regex.IsMatch(name, namePattern);
        }

        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string emailPattern = @"^[a-zA-Z0-9._%+-]{3,20}@[a-zA-Z0-9.-]{2,20}\.[a-zA-Z]{2,10}$";

            return true;
        }

        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$";

            return Regex.IsMatch(password, passwordPattern);
        }
    }
}
