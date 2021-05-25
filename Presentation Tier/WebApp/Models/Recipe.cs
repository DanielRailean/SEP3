using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Recipe
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<RecipeItem> Ingredient { get; set; }
        [Required]
        public double Calories { get; set; }
        [Required]
        public int MinutesToMake { get; set; }
        [Required]
        public string NutritionType { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [JsonIgnore]
        public int Amount { get; set; }
    }
}