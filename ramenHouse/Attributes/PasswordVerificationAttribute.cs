using ramenHouse.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ramenHouse.Attributes
{
    public class PasswordVerificationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var emailProperty = validationContext.ObjectType.GetProperty("Email");
            if (emailProperty == null)
            {
                return new ValidationResult("The instance does not contain an 'Email' property");
            }

            var email = emailProperty.GetValue(validationContext.ObjectInstance, null) as string;
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                return new ValidationResult("Invalid email or password");
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, value as string);

            if (result != PasswordVerificationResult.Success)
            {
                return new ValidationResult("Invalid email or password");
            }

            return ValidationResult.Success;
        }
    }
}