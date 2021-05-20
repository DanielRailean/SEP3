using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IAdminService
    {
        public Task<Administrator> ValidateAdministrator(string email, string password);
    }
}