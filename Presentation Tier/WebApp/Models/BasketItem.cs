using System.Collections.Generic;

namespace WebApp.Models
{
    public class BasketItem
    {
        public BasketItem(int amount, int recipeId)
        {
            Amount = amount;
            RecipeId = recipeId;
        }

        public int Amount { get; set; }
        public int RecipeId { get; set; }
        
    }
}