namespace ramenHouse.ViewModels.Admin
{
    public class MealAllergiesEditFormViewModel
    {
        public int mealId { get; set; } 
        public List<AllergyViewModel> mealAllergies { get; set; } = new List<AllergyViewModel>();   
    }
}
