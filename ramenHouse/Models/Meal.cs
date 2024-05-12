using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ramenHouse.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; } 

        public string ImgUrl { get; set; }  

        public int Rating { get; set; }

        public decimal Price { get; set; }


        public DateTime CreationTime { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();



    }
}
