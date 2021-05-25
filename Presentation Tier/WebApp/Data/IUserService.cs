using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<User> ValidateUserAsync(string email, string password);
        Task UpdateUserAsync(User user,string password);
        Task RemoveUserAsync(User user);
        User GetCurrentUser();
    }
}