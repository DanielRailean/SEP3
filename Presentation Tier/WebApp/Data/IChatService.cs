using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IChatService
    {
        //Read
        Task<IList<ChatUser>> GetOnlineAdmins();
        Task<IList<ChatUser>> GetOnlineUsers();
        Task<IList<ChatRoom>> GetChatRooms();
        Task<ChatRoom> GetRoom(long userId,bool isAdmin,string connectionId);
        Task<ChatRoom> GetRoom(string roomId);
        Task<IList<Message>> GetGroupMessages(string roomId);
        Task<ChatRoom> NextInQueue(string adminConnectionId);
        bool IsAdminOnline(string connectionId);
        bool IsUserOnline(string connectionId);
        Task<string> GetUpdates(long userId,bool isAdmin);
        Task<ChatUser> GetUser(string connectionId);
        Task<int> GetUserStatus(long userId);
        Task<ChatUser> GetUserById(long userId);
        bool IsUserConnected(long userId);
        bool IsAdminConnected(long userId);
        Task<ChatRoom> AskQuestion(string question,string connectionId);

        
        //Create
        // Task AddChatUser(ChatUser user);
        Task AddMessage(Message message, string roomId);
        Task ConnectToChat(long userId, bool isAdmin, string connectionId, string name);
        Task GoOffline(long userId, bool isAdmin, string connectionId, string name);

        //Update
        Task ChangeUserStatus(string connectionId, int status);
        Task ChangeUserRoom(string userConnectionId, string roomId);
        Task<ChatRoom> ConnectToRoom(string userConnectionId, string roomId);
        Task<ChatRoom> ExitRoom(string userConnectionId);
        Task<ChatRoom> ReconnectToChat(int userId,string userConnectionId);
        
        //Delete
        Task<ChatRoom> DisconnectUser(string userConnectionId);
        Task StopChat(string connectionId);
        Task RemoveRoom(string roomId);

    }
}