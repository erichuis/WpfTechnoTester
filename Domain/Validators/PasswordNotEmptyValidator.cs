using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace Domain.Validators
{
    public class PasswordNotEmptyValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Password can not be empty");
            }

            if(string.IsNullOrEmpty(new NetworkCredential(string.Empty, (SecureString)value).Password))
            {
                return new ValidationResult("Password can not be empty");
            }
            return ValidationResult.Success;
        }
    }
}