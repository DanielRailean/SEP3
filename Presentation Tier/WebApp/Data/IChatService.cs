using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IChatService
    {
        //Read
        Task<IList<ChatUser>> GetAdmins();
        Task<IList<ChatRoom>> GetChatRooms();
        Task<ChatRoom> GetRoom(long userId,bool isAdmin,string connectionId,string name);
        Task<IList<Message>> GetGroupMessages(string roomId);
        Task<ChatRoom> NextInQueue(string adminConnectionId);
        bool AdminConnected(string connectionId);
        Task<string> GetUpdates(string connectionId,bool isAdmin);
        Task<ChatUser> GetUser(string connectionId);
        
        //Create
        Task AddChatUser(ChatUser user);
        Task AddMessage(Message message, string roomId);
        
        //Update
        Task ChangeUserStatus(string connectionId, int status);
        
        //Delete
        Task DisconnectUser(string connectionId);
        Task StopChat(string connectionId);

    }
}