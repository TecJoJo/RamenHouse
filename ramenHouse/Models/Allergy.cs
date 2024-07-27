using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ramenHouse.Models
{
    public class Allergy
    {
        [Key]
        public int AllergyId { get; set; }
        public string Name { get; set; }

        public string Abbreviation { get; set; }
        [JsonIgnore]
        public ICollection<Meal> Meals { get; set; }
    }
}
