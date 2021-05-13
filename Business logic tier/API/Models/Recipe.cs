using System.Collections.Generic;

namespace API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Ingredient> Ingredient { get; set; }
        public double Calories { get; set; }
        public string NutritionType { get; set; }
        public string FoodType { get; set; }
        public double Price { get; set; }
    }
}