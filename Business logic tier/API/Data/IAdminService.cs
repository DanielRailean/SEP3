using API.Models;

namespace API.Data
{
    public interface IAdminService
    {
        public Administrator ValidateAdministrator(string email, string password);
    }
}