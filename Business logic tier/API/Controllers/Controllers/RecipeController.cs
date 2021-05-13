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
        public async Task<ActionResult<Recipe>> AddRecipe(Recipe recipe)
        {
            try
            {
                recipeService.AddRecipe(recipe);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    
        [HttpGet("GetRecipe")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            try
            {
                Recipe valid = recipeService.GetRecipe(id);
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
                IList<Recipe> valid = recipeService.GetAllRecipes();
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Recipe>> UpdateRecipe(Recipe recipe)
        {
            try
            {
                Recipe valid = recipeService.UpdateRecipe(recipe);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Recipe>> RemoveRecipe(Recipe recipe)
        {
            try
            {
                Recipe valid = recipeService.RemoveRecipe(recipe);
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