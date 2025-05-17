using Microsoft.AspNetCore.Identity;
using ramenHouse.Models;
using System.ComponentModel.DataAnnotations;

namespace ramenHouse.ViewModels
{
    public class UserViewModel:IValidatableObject
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                yield return new ValidationResult("Invalid email or password");
                yield break;
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, Password);

            if (result != PasswordVerificationResult.Success)
            {
                yield return new ValidationResult("Invalid email or password");
            }
        }
    }
}
