using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClientReef
{
    public class ValidBirthYear : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                DateTime date = DateTime.Parse(value.ToString());
                if (date.Year < 1900)
                    return new ValidationResult(false, "Too old");
                if (date.Month > 12 && date.Month < 1)
                    return new ValidationResult(false, "No way!");
                if (date.Day > 31 && date.Day < 1)
                    return new ValidationResult(false, "No way!");
                if (date.Year > DateTime.Now.Year)
                    return new ValidationResult(false, "No way!");
                else if(date.Year == DateTime.Now.Year)
                {
                    if (date.Month == DateTime.Now.Month)
                    {
                        if (date.Day >= DateTime.Now.Day)
                            return new ValidationResult(false, "No Way!");

                    }
                    else if (date.Month > DateTime.Now.Month)
                        return new ValidationResult(false, "No Way!");


                }

            }
            catch (Exception ex )
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();
                if (name.Length < Min)
                    return new ValidationResult(false, "name is too short");
                if (name.Length > Max)
                    return new ValidationResult(false, "name is too long");
                if (!Char.IsUpper(name[0]))
                    return new ValidationResult(false, "The first letter must be  a big letter");
                for (int i = 0; i < name.Length; i++)
                {
                    if (Char.IsWhiteSpace(name[i]) && Char.IsWhiteSpace(name[i + 1]))
                    {
                        return new ValidationResult(false, "You cant have double space");

                    }
                    if (!Char.IsLetter(name[i]) && !Char.IsWhiteSpace(name[i]))
                    {
                        return new ValidationResult(false, "must be a letter");
                    }
                }

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string password = value.ToString();
                bool big = false;
                bool small = false;
                bool digit = false;
                bool symbalolo = false;
                if (password.Length < 6)
                    return new ValidationResult(false, "Password is too short");
                if (password.Length > 16)
                    return new ValidationResult(false, "Password is too long");
                string symbolis = "#$-~!?";
                for (int i = 0; i < password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(password[i]) && symbolis.IndexOf(password[i]) == -1)
                        return new ValidationResult(false, "the password can contain only letters,digits and" + symbolis);
                    if (Char.IsUpper(password[i]))
                        big = true;
                    else
                        small = true;
                    if (Char.IsDigit(password[i]))
                        digit = true;
                    if (symbolis.IndexOf(password[i]) != -1)
                        symbalolo = true;

                }
                if (!(big && small && digit && symbalolo))
                    return new ValidationResult(false, "the password must contain at least one big letter, one small letter, one digit and one symbols ");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Error: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidEmail : ValidationRule
    {
        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {

            string email = value.ToString().Trim();
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool res = regex.IsMatch(email);

            if (!res)
            {
                return new ValidationResult(res, "Email not valid");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidPhoneNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string PhoneNumber = value.ToString();

                if (PhoneNumber.Length != 10)
                    return new ValidationResult(false, "Phone number must have 10 digits");

                if (PhoneNumber[0] != '0' || PhoneNumber[1] != '5')
                    return new ValidationResult(false, "Illegal beginning");

                if (PhoneNumber.IndexOf(" ") != -1)
                    return new ValidationResult(false, "Phone number shouldn't include spaces");

                for (int i = 0; i < PhoneNumber.Length; i++)
                {
                    if (!(Char.IsDigit(PhoneNumber[i])))
                        return new ValidationResult(false, "Numbers only!");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
}
