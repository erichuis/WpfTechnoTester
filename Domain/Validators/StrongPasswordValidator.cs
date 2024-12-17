using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;

namespace Domain.Validators
{
    public class StrongPasswordValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Password is not strong enough!");
            }

            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if( validateGuidRegex.IsMatch(new NetworkCredential(string.Empty, (SecureString)value).Password))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Password is not strong enough!");
        }
    }
}