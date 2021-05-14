using System.Collections;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Ingredient> Ingredient { get; set; }
        public double Calories { get; set; }
        public NutritionType Type { get; set; }
        public double Price { get; set; }
    }
}