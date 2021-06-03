using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    /// <summary>
    /// Model for recipe in the data access tier.
    /// </summary>
    public class DataRecipe
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
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
        [Required]
        public string IngredientIdList { get; set; }
        [Required]
        public string IngredientQuantityList { get; set; }
    }
}