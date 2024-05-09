namespace ramenHouse.ViewModels
{
    public class MenuViewModel
    {
        public List<MealViewModel> Meals { get; set; } = new List<MealViewModel>();

        public List<string> Categories { get; set; } = new List<string>();
    }
}
