using ramenHouse.Models;

namespace ramenHouse.ViewModels
{
    public class HomeViewModel
    {
        public List<MealViewModel> featuredMeals = new List<MealViewModel>();
        public List<MealViewModel> onSellMeals = new List<MealViewModel>();
    }
}
