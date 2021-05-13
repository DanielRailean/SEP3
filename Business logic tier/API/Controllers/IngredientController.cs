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
    
    public class IngredientController : ControllerBase, IIngredientController
    {
        private IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }
            
        [HttpPost]
        public async Task<ActionResult<Ingredient>> AddIngredient(Ingredient ingredient)
        {
            try
            {
                ingredientService.AddIngredient(ingredient);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetIngredient")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            try
            {
                Ingredient valid = ingredientService.GetIngredient(id);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAllIngredients")]
        public async Task<ActionResult<IList<Ingredient>>> GetAllIngredients()
        {
            try
            {
                IList<Ingredient> valid = ingredientService.GetAllIngredients();
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpPut]
        public async Task<ActionResult<Ingredient>> UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient valid = ingredientService.UpdateIngredient(ingredient);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Ingredient>> RemoveIngredient(Ingredient ingredient)
        {
            try
            {
                Ingredient valid = ingredientService.RemoveIngredient(ingredient);
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