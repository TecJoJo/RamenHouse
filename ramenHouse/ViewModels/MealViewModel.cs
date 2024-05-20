namespace ramenHouse.ViewModels
{
    public class MealViewModel
    {
     
        public int? MealId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Rating { get; set; } 

        public decimal BasePrice { get; set; }  



        public string AllergiesShort { get; set; } = string.Empty;

        public string AllergiesLong { get; set; } = string.Empty;

        public string Category {  get; set; } = string.Empty;

        public decimal Discount { get; set; } 

        public bool IsFeatured { get; set; }
    }
}
