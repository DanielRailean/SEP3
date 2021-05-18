namespace WebApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Quantity { get; set; }
        public double Calories { get; set; }
    }
}