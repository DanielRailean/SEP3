﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            try
            {
                userService.RegisterUser(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetUser")]
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
        
        // [HttpGet("GetAllUsers")]
        // public async Task<ActionResult<IList<User>>> GetAllUsers()
        // {
        //     try
        //     {
        //         IList<User> valid = await userService.GetAllUsers();
        //         return Ok(valid);
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e.Message);
        //         return StatusCode(500, e.Message);
        //     }
        // }

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