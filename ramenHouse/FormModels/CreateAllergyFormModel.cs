using System.ComponentModel.DataAnnotations;

namespace ramenHouse.FormModels
{
    public class CreateAllergyFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }
    }
}
