namespace ramenHouse.ViewModels.Admin
{
    public class AdminMealViewModel
    {
        public int MealId { get; set; }
        public string DishName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Rating { get; set; }

        public AllergiesInlineEditViewModel AllergiesEditInfo { get; set; } = new AllergiesInlineEditViewModel();

        public decimal BasePrice { get; set; }

        public decimal Discount { get; set; }

        public decimal SalePrice { get; set; }  



        public string Category { get; set; } = string.Empty;


        public bool IsFeatured { get; set; }

        public int DeleteId { get; set; }
    }
}
