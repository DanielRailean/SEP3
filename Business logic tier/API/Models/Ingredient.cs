using System.Text.Json.Serialization;

namespace API.Models
{
    public class Ingredient
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public double Calories { get; set; }
    }
}