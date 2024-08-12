using System.ComponentModel.DataAnnotations;

namespace ramenHouse.FormModels
{
    public class MealCreateFormModel
    {

        [Required]
        public string Name { get; set; }
        public bool IsFeatured { get; set; } = false;
        
        public decimal BasePrice { get; set; }
        
        public decimal Discount { get; set; } 
        public List<int> AllergyIds { get; set; } = new List<int>();
        
        public string? Description { get; set; } = string.Empty;
        public IFormFile? imgFile { get; set; } 
        //tempararylly leave the category blank, because of the complexity reason


    }
}
