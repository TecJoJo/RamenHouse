using System.ComponentModel.DataAnnotations;

namespace ramenHouse.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [UniqueEmail]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
