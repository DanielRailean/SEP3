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
        Task<ChatRoom> GetConnection(long userId,bool isAdmin,string connectionId,string name);
        Task<IList<Message>> GetGroupMessages(string roomId);
        Task<ChatRoom> NextInQueue(string adminConnectionId);
        bool IsAdminOnline(string connectionId);
        bool IsUserOnline(string connectionId);
        Task<string> GetUpdates(string connectionId,bool isAdmin);
        Task<ChatUser> GetUser(string connectionId);
        bool IsUserConnected(long userId);
        bool IsAdminConnected(long userId);
        
        //Create
        Task AddChatUser(ChatUser user);
        Task AddMessage(Message message, string roomId);
        Task ConnectToChat(long userId, bool isAdmin, string connectionId, string name);
        Task GoOffline(long userId, bool isAdmin, string connectionId, string name);

        //Update
        Task ChangeUserStatus(string connectionId, int status);
        
        //Delete
        Task DisconnectUser(string connectionId);
        Task StopChat(string connectionId);

    }
}