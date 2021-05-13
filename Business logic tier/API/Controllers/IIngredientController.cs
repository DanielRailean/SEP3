using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public interface IIngredientController
    {
        Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredient);
        Task<ActionResult<Ingredient>> GetIngredient([FromBody] int id);
        Task<ActionResult<IList<Ingredient>>> GetAllIngredients();
        Task<ActionResult<Ingredient>> UpdateIngredient([FromBody] Ingredient ingredient);
        Task<ActionResult<Ingredient>> RemoveIngredient([FromBody] Ingredient ingredient);

    }
}