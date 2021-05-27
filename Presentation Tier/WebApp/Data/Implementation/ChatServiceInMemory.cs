using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    public class ChatServiceInMemory : IChatService
    {
        private IList<ChatUser> AllUsers;
        private IList<ChatUser> Admins;
        private IList<ChatRoom> ChatRooms;
        private int maxUsers;
        private int maxAdmins;

        public ChatServiceInMemory()
        {
            AllUsers = new List<ChatUser>();
            Admins = new List<ChatUser>();
            ChatRooms = new List<ChatRoom>();
            maxUsers = 0;
            maxAdmins = 0;
        }
        
        public async Task<ChatUser> RegisterUser(ChatUser chatUser)
        {
            chatUser.Id = (++maxUsers);
            AllUsers.Add(chatUser);
            return chatUser;
        }
        

        public async Task<ChatUser> RemoveUser(ChatUser chatUser)
        {
            ChatUser toRemove = AllUsers.FirstOrDefault(u => u.ConnectionId == chatUser.ConnectionId);
            if (toRemove == null) throw new Exception("User does not exist");
            AllUsers.Remove(toRemove);
            return toRemove;
        }

        public async Task<ChatUser> RemoveAdmin(ChatUser admin)
        {
            ChatUser toRemove = Admins.FirstOrDefault(u => u.ConnectionId == admin.ConnectionId);
            if (toRemove == null) throw new Exception("User does not exist");
            Admins.Remove(toRemove);
            return toRemove;
        }

        public async Task<IList<ChatUser>> GetAllUsers()
        {
            return AllUsers;
        }

        public async Task<ChatUser> AddAdmin(ChatUser admin)
        {
            admin.Id = (++maxAdmins);
            Admins.Add(admin);
            return admin;
        }

        public async Task<ChatUser> GetUserByConnectionId(string connectionId)
        {
            ChatUser toRemove = AllUsers.FirstOrDefault(u=>u.ConnectionId.Equals(connectionId));
            return toRemove;
        }
        
        public async Task<ChatUser> GetAdminByConnectionId(string connectionId)
        {
            ChatUser toRemove = Admins.FirstOrDefault(u=>u.ConnectionId.Equals(connectionId));
            return toRemove;
        }

        public async Task ChangeAdminStatus(string roomId, bool isInRoom)
        {
            ChatUser toRemove = Admins.FirstOrDefault(u=>u.ChatRoom.Equals(roomId));
            if(toRemove!=null)
            {
                toRemove.IsInChat = isInRoom;
            }
        }
        
        public async Task ChangeUserStatus(string roomId, bool isInRoom)
        {
            ChatUser toRemove = AllUsers.FirstOrDefault(u=>u.ChatRoom.Equals(roomId));
            if(toRemove!=null)
            {
                toRemove.IsInChat = isInRoom;
            }
        }

        public async Task<ChatUser> NextInQueue()
        {
            ChatUser toRemove = AllUsers.FirstOrDefault(u=>u.IsInChat==false);
            return toRemove;
        }

        public async Task<ChatUser> GetGroupAdmin(string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            if(room!=null)
            {
                return room.Admin;
            }

            return null;
        }

        public async Task<ChatUser> GetGroupUser(string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            if(room!=null)
            {
                return room.Customer;
            }

            return null;
        }

        public async Task<IList<Message>> GetGroupMessages(string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            if(room!=null)
            {
                return room.Messages;
            }

            return null;
        }

        public async Task<IList<ChatUser>> GetAllAdmins()
        {
            return Admins;
        }
        
    }
}