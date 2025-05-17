using ramenHouse.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ramenHouse.Models
{
    public class User
    {

        public User() {
            this.CreatedDate = DateTime.Now;
        }
        [Key]
        public int UserId { get; set; }

        

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        
    }
}
