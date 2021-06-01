using System;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Controllers
{
    /// <summary>
    /// Controller for providing endpoints to user services.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Adding a new user to storage.
        /// </summary>
        /// <param name="user">User item to add.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            try
            {
                User returned = await userService.RegisterUser(user);
                return Ok(returned);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets the user if the parameters are valid.
        /// </summary>
        /// <param name="email">E-mail of user.</param>
        /// <param name="password">Password of user.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                Console.Write(email+password);
                User valid = await userService.ValidateUser(email, password);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Updates a specific user if the parameters are valid.
        /// </summary>
        /// <param name="user">User item to update.</param>
        /// <param name="password">Password of user to validate.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody]User user,[FromQuery] string password)
        {
            try
            {
                User valid = await userService.UpdateUser(user,password);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific user.
        /// </summary>
        /// <param name="user">User item to delete.</param>
        /// <response code="200">Successful transaction.</response>
        /// <response code="400">Missing values.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete]
        public async Task<ActionResult<User>> RemoveUser(User user)
        {
            try
            {
                User valid = await userService.RemoveUser(user);
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