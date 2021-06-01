using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Controllers
{
    /// <summary>
    /// Controller for providing the endpoints for ingredient services.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase, IIngredientController
    {
        private IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }
           
        /// <summary>
        /// For adding a new ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient item to add.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        public async Task<ActionResult<Ingredient>> AddIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient returned = await ingredientService.AddIngredient(ingredient);
                return Ok(returned);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Get a specific ingredient by its id.
        /// </summary>
        /// <param name="id">Id of the ingredient to get.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("GetIngredient")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            try
            {
                Ingredient valid = await ingredientService.GetIngredient(id);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all the ingredients that are stored.
        /// </summary>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("GetAllIngredients")]
        public async Task<ActionResult<IList<Ingredient>>> GetAllIngredients()
        {
            try
            {
                IList<Ingredient> valid = await ingredientService.GetAllIngredients();
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Updates a specific ingredient that is stored.
        /// </summary>
        /// <param name="ingredient">Ingredient item to update.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        public async Task<ActionResult<Ingredient>> UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient valid = await ingredientService.UpdateIngredient(ingredient);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific ingredient that is passed.
        /// </summary>
        /// <param name="ingredient">Ingredient item to delete.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete]
        public async Task<ActionResult<Ingredient>> RemoveIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient valid = await ingredientService.RemoveIngredient(ingredient);
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