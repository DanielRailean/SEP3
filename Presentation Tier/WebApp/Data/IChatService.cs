using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IChatService
    {
        //Read
        Task<IList<ChatRoom>> GetChatRooms();
        Task<ChatRoom> GetRoom(string roomId);
        Task<ChatRoom> NextInQueue(string adminConnectionId);
        Task<ChatUser> GetUser(string connectionId);
        Task<int> GetUserStatus(long userId);
        Task<ChatUser> GetUserById(long userId);
        Task<ChatRoom> AskQuestion(string question,string connectionId);

        
        //Create
        Task AddMessage(Message message, string roomId);
        Task ConnectToChat(long userId, bool isAdmin, string connectionId, string name);

        //UPDATE
        Task<ChatRoom> ConnectToRoom(string userConnectionId, string roomId);
        Task<ChatRoom> ExitRoom(string userConnectionId);
        Task<ChatRoom> ReconnectToChat(int userId,string userConnectionId);
        
        //Delete
        Task<ChatRoom> DisconnectUser(int userId);
        Task RemoveRoom(string roomId);

    }
}