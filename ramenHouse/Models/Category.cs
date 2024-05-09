using System.ComponentModel.DataAnnotations;

namespace ramenHouse.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }



        public string Description { get; set; } 

        
    }
}
