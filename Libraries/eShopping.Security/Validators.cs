
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace eShopping.Security
{
    public static class Validators
    {

        public static bool IsValidEmail(this string email)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static void ValidateEmail(this string email)
        {
            if (!IsValidEmail(email))
            {
                throw new ValidationException("Email is not valid.");
            }
        }


        public static bool PasswordHasUpperCase(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string pattern = @"[A-Z]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool PasswordHasLowerCase(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string pattern = @"[a-z]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool PasswordHasDigit(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string pattern = @"\d";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool PasswordHasSpecial(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string pattern = @"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool PasswordHasSpacesOrFlaggedSpecial(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string pattern = @"^[^£# “”]*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static void ValidatePassword(this string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                throw new ValidationException("Password must not be empty.");

            if (password.Length < 8)
                throw new ValidationException("Password must be at least 8 characters long.");

            if (!PasswordHasUpperCase(password))
                throw new ValidationException("Password must contain at least one uppercase letter.");

            if (!PasswordHasLowerCase(password))
                throw new ValidationException("Password must contain at least one lowercase letter.");

            if (!PasswordHasDigit(password))
                throw new ValidationException("Password must contain at least one numeric character.");

            if (!PasswordHasSpecial(password))
                throw new ValidationException("Password must contain at least one special character.");

            if (!PasswordHasSpacesOrFlaggedSpecial(password))
                throw new ValidationException("Password must not contain the following characters £ # “” or spaces.");
        }

    }
}
