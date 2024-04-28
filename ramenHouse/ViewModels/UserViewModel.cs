using ramenHouse.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ramenHouse.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}
