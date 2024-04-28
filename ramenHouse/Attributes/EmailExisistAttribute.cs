using System.ComponentModel.DataAnnotations;
using System.Linq;
using ramenHouse.Data;
using ramenHouse.Models;

public class EmailExistAttribute : ValidationAttribute
{
    //this class is only used for registration not for login
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var _DbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        if (value != null)
        {
            var entity = _DbContext.Users.SingleOrDefault(e => e.Email == value.ToString());

            if (entity != null)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Invalid email or password", new List<string>());
    }



}
