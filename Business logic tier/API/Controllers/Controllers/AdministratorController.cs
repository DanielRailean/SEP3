using System;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AdministratorController : ControllerBase, IAdministratorController
    {
        private IAdminService adminService;

        public AdministratorController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        
        [HttpGet]
        public async Task<ActionResult<Administrator>> ValidateAdministrator(string email, string password)
        {
            try
            {
                Administrator valid = await adminService.ValidateAdministrator(email,password);
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