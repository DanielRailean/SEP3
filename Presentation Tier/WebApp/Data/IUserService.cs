using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IUserService
    {
        Task AddUser(User user);
    }
}