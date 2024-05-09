using System.ComponentModel.DataAnnotations;

namespace ramenHouse.Models
{
    public class Allergy
    {
        [Key]
        public int AllergyId { get; set; }
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
