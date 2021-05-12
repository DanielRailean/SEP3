using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<User> ValidateUserAsync(string email, string password);
        Task<User> UpdateUserAsync(User user);
        Task RemoveUserAsync(int userId);
    }
}