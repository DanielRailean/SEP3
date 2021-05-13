using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Data
{
    public interface IAdministratorController
    {
        Task<ActionResult<Administrator>> ValidateAdministrator([FromBody] string email, string password);

    }
}