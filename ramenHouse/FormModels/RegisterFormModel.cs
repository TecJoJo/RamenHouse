using System.ComponentModel.DataAnnotations;

namespace ramenHouse.FormModels
{
    public class RegisterFormModel : IValidatableObject
    {
        [Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult(
                    "Passwords do not match.",
                    new[] { nameof(Password), nameof(ConfirmPassword) }
                );
            }
        }
    }
}
