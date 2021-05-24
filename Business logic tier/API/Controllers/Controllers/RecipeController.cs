using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class RecipeController : ControllerBase, IRecipeController
    {
        private IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody]Recipe recipe)
        {
            try
            {
               Recipe returned= await recipeService.AddRecipe(recipe);
                return Ok(returned);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    
        [HttpGet("GetRecipe")]
        public async Task<ActionResult<Recipe>> GetRecipe([FromQuery]int id)
        {
            try
            {
                Recipe valid = await recipeService.GetRecipe(id);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllRecipes")]
        public async Task<ActionResult<IList<Recipe>>> GetAllRecipes()
        {
            try
            {
                IList<Recipe> valid = await recipeService.GetAllRecipes();
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                Recipe valid = await recipeService.UpdateRecipe(recipe);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Recipe>> RemoveRecipe([FromBody]Recipe recipe)
        {
            try
            {
                Recipe valid = await recipeService.RemoveRecipe(recipe);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }

}