using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IChatService
    {
        Task<ChatUser> RegisterUser(ChatUser chatUser);
        Task<ChatUser> RemoveUser(ChatUser chatUser);
        Task<ChatUser> RemoveAdmin(ChatUser admin);
        Task<IList<ChatUser>> GetAllUsers();
        Task<IList<ChatUser>> GetAllAdmins();
        Task<ChatUser> AddAdmin(ChatUser admin);
        
        Task<ChatUser> GetUserByConnectionId(string connectionId);
        Task<ChatUser> GetAdminByConnectionId(string connectionId);
        Task ChangeAdminStatus(string roomId, bool isInRoom);
        Task ChangeUserStatus(string roomId, bool isInRoom);
        Task<ChatUser> NextInQueue(string adminConnectionId);

        Task<ChatUser> GetGroupAdmin(string roomId);
        Task<ChatUser> GetGroupUser(string roomId);

        Task<string> CreateGroup(ChatUser user);

        Task AddMessage(Message message, string roomId);
        Task<IList<Message>> GetGroupMessages(string roomId);

        Task<IList<ChatRoom>> GetChatRooms();

        Task<string> GetUpdates(string connectionId);
    }
}