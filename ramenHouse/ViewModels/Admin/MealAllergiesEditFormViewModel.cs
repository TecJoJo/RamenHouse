namespace ramenHouse.ViewModels.Admin
{
    public class MealAllergiesEditFormViewModel
    {

        public int MealId { get; set; } 
        public List<AllergyViewModel> MealAllergies { get; set; } = new List<AllergyViewModel>();
        public List<AllergyViewModel>? AllAllergies { get; set; } = new List<AllergyViewModel>();
    }
}
