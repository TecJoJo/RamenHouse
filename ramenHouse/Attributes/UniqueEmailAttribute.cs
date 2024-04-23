using System.ComponentModel.DataAnnotations;
using System.Linq;
using ramenHouse.Data;
using ramenHouse.Models;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        if (value != null)
        {
            var entity = _context.Users.SingleOrDefault(e => e.Email == value.ToString());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
        }

        return new ValidationResult("Email filed can not be leave empty");
    }


    private string GetErrorMessage(string email)
    {
        return $"Email {email} is already in use.";
    }
}
