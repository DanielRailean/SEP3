using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user);
        Task<User> ValidateUser(string email, string password);
        Task<User> UpdateUser(User user, string password);
        Task<User> RemoveUser(User user);
    }
}