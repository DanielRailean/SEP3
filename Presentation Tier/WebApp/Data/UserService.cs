using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using WebApp.Models;

namespace WebApp.Data
{
    public class UserService : IUserService
    {
        private const string uri = "https://localhost:5003/Users";
        private HubConnection hubConnection;

        public UserService()
        {
            
        }
        
        public async Task AddUser(User user)
        {
            // var userAsJson = JsonSerializer.Serialize(user);
            // HttpContent content = new StringContent(userAsJson,
            //     Encoding.UTF8,
            //     "application/json");
            // await client.PostAsync(uri, content);
        }
    }
}