using System.Xml.Serialization;

namespace ramenHouse.ViewModels
{
    public class AdminViewModel
    {
        public List<MealViewModel> meals { get; set; } = new List<MealViewModel>();

       
    }
}
