namespace ramenHouse.FormModels
{
    public class UpdateMealFormModel
    {
        
            public int mealId { get; set; }
            public string dishName { get; set; }
            public string description { get; set; }
            public string imageUrl { get; set; }
            public int rating { get; set; }
            public string allergies { get; set; }
            public decimal basePrice { get; set; }
            public decimal discount { get; set; }
            public bool isFeatured { get; set; }

    }
}
