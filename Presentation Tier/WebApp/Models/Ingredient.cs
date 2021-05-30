namespace WebApp.Models
{
    /// <summary>
    /// Model for the ingredient.
    /// </summary>
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        /// <summary>
        /// Unit of measure (e.g. "g", "gram", "cup", "liter", etc.)
        /// </summary>
        public string UnitOfMeasure { get; set; }
        
        /// <summary>
        /// Amount that is always zero, except when it is added to a recipe.
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Calories that are for every 100 g of intake.
        /// </summary>
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