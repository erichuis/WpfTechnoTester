using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class PasswordVerifiedValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value , ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as User;

            if (value == null && (user == null || user.PasswordVerified == null))
            {
                return ValidationResult.Success;
            }
            else
            {
                if(new NetworkCredential(string.Empty, value as SecureString).Password.Equals(
                new NetworkCredential(string.Empty, user.PasswordVerified).Password))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Passwords are not equal!");
            }

        }
    }
}
