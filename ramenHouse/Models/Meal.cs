    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ramenHouse.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; } 

        public string ImgUrl { get; set; }  

        public int Rating { get; set; }

        public decimal BasePrice { get; set; }

        


        public DateTime CreationTime { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        [JsonIgnore]
        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

        public bool IsFeatured { get; set; }

        public decimal Discount { get; set; }


    }
}
