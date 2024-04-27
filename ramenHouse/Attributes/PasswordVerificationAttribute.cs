using ramenHouse.Models;
using System.ComponentModel.DataAnnotations;

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

        if (user == null || user.Password != value as string)
        {
            return new ValidationResult("Invalid email or password");
        }

        return ValidationResult.Success;
    }
}
}
