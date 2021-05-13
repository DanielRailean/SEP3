using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Data
{
    public interface IRecipeController
    {
        Task<ActionResult<Recipe>> AddRecipe([FromBody] Recipe recipe);
        Task<ActionResult<Recipe>> GetRecipe([FromBody] int id);
        Task<ActionResult<IList<Recipe>>> GetAllRecipes();
        Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipe);
        Task<ActionResult<Recipe>> RemoveRecipe([FromBody] Recipe recipe);
    }
}