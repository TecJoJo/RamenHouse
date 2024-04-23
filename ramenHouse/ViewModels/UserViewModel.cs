using ramenHouse.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ramenHouse.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailExist]
        public string Email { get; set; }
        [Required]
        [PasswordVerification]
        public string Password { get; set; }
    }
}
