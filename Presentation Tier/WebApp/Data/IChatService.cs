using System.Collections.Generic;
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
        Task<ChatUser> NextInQueue();

        Task<ChatUser> GetGroupAdmin(string roomId);
        Task<ChatUser> GetGroupUser(string roomId);

        Task<IList<Message>> GetGroupMessages(string roomId);
    }
}