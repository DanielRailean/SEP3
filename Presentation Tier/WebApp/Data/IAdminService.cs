using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IAdminService
    {
        Task<Administrator> ValidateAdminAsync(string email, string password);
    }
}