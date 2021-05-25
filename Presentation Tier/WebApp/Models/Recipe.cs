using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Ingredient> Ingredient { get; set; }
        public double Calories { get; set; }
        public int MinutesToMake { get; set; }
        public NutritionType Type { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        [JsonIgnore]
        public int Amount { get; set; }
        [JsonIgnore]
        public int RecipeId { get; set; }
    }
}