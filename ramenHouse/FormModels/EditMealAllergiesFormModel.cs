namespace ramenHouse.FormModels
{
    public class EditMealAllergiesFormModel
    {
        public int MealId { get; set; }

        public List<string> AllergyIds{ get; set; } = new List<string>();   
    }
}
