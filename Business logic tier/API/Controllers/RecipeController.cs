using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class RecipeController : ControllerBase , IRecipeService
    {
        private IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }
        
        public Recipe AddRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Recipe GetRecipe(int id)
        {
            throw new System.NotImplementedException();
        }

        public Recipe UpdateRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Recipe RemoveRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}