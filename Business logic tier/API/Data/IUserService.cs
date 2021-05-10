using API.Models;

namespace API.Data
{
    public interface IUserService
    {
         User RegisterUser(User user);
         User ValidateUser(string email, string password);
         User UpdateUser(User user, string password);
         User RemoveUser(User user);
    }
}