using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public interface IUserController
    {
        Task<ActionResult<User>> RegisterUser([FromBody] User user);
        Task<ActionResult<User>> ValidateUser([FromQuery] string? email, [FromQuery] string? password);
        Task<ActionResult<User>> UpdateUser([FromBody]User user,[FromQuery] string password);
        Task<ActionResult<User>> RemoveUser([FromBody] User user);
    }
}