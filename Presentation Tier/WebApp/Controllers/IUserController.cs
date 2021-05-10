using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public interface IUserController
    {
        Task<ActionResult<User>> AddUser(User user);
    }
}