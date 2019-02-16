using System.Security;
using System.Text.RegularExpressions;

namespace SharpDj.Input
{
    public class UserValidation
    {
        public static bool EmailIsValid(string email)
        {
            return Regex.IsMatch(email, @"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z")
                   && Regex.IsMatch(email, @"^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*");
        }

        public static bool LoginIsValid(string login, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(login)) return true;
            if (login.Length >= minLength && login.Length <= maxLength)
                return true;
            return false;
        }

        public static bool PasswordIsValid(SecureString password, int minLength, int maxLength)
        {
            //new System.Net.NetworkCredential(string.Empty, password).Password; instead of method just for little security
            if (string.IsNullOrEmpty(new System.Net.NetworkCredential(string.Empty, password).Password)) return true;
            if (new System.Net.NetworkCredential(string.Empty, password).Password.Length >= minLength &&
                new System.Net.NetworkCredential(string.Empty, password).Password.Length <= maxLength)
                return true;
            return false;

        }
    }
}
