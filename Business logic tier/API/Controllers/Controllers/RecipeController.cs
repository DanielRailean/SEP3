using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Controllers
{
    /// <summary>
    /// Controller that provides endpoints for recipe services.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase, IRecipeController
    {
        private IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }
        
        /// <summary>
        /// For adding a new recipe.
        /// </summary>
        /// <param name="recipe">Recipe item to add.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
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
    
        /// <summary>
        /// Gets a specific recipe item by id.
        /// </summary>
        /// <param name="id">Id of recipe to get.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
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

        /// <summary>
        /// Get all the recipe items in a list from storage.
        /// </summary>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
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

        /// <summary>
        /// Update a specific recipe item.
        /// </summary>
        /// <param name="recipe">Recipe item to update.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
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

        /// <summary>
        /// Deletes a specific recipe.
        /// </summary>
        /// <param name="recipe">Recipe item to delete.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
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