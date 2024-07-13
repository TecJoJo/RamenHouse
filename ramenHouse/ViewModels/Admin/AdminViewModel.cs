using ramenHouse.ViewModels.Admin;
using System.Xml.Serialization;

namespace ramenHouse.ViewModels
{
    public class AdminViewModel
    {
        public List<AdminMealViewModel> meals { get; set; } = new List<AdminMealViewModel>();

       
    }
}
