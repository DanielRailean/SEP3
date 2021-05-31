namespace WebApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Quantity { get; set; }
        public double Calories { get; set; }

        public Ingredient(string name, string unitOfMeasure, double calories)
        {
            Quantity = 0;
            Name = name;
            UnitOfMeasure = unitOfMeasure;
            Calories = calories;
        }

        public override string ToString()
        {
            string s = this.Name;
            return s;
        }
    }
}