using ramenHouse.Models;
using System.ComponentModel.DataAnnotations;

namespace ramenHouse.Attributes
{
    public class PasswordVerificationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _DbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var emailProperty = validationContext.ObjectType.GetProperty("Email");

            if (emailProperty == null)
            {
                return new ValidationResult("The instance does not contain an 'Email' property");
            }

            var email = emailProperty.GetValue(validationContext.ObjectInstance, null) as string;

            var user = _DbContext.Users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if(value == user.Password) return ValidationResult.Success;
            }

            return new ValidationResult("Email or password is incorrect, please have a recheck");


        }
    }
}
