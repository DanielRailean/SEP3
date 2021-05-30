namespace WebApp.Models
{
    /// <summary>
    /// Model for items in the basket, so only
    /// an amount and id pair will be stored in the orders.
    /// </summary>
    public class BasketItem
    {
        public BasketItem(int amount, int recipeId)
        {
            Amount = amount;
            RecipeId = recipeId;
        }

        /// <summary>
        /// The number of same recipes.
        /// </summary>
        public int Amount { get; set; }
        
        /// <summary>
        /// Id to match with recipe.
        /// </summary>
        public int RecipeId { get; set; }
        
    }
}