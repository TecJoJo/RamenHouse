namespace ramenHouse.FormModels
{
    public class MealCreateFormModel
    {
       
            public string Name { get; set; }
            public bool IsFeatured { get; set; }
            public decimal SellPrice { get; set; }
            public decimal BasePrice { get; set; }
            public decimal Discount { get; set; }
            public List<int> AllergyIds { get; set; }
            public string Description { get; set; }
            public IFormFile imgFile { get; set; }
            //tempararylly leave the category blank, because of the complexity reason
        

    }
}
