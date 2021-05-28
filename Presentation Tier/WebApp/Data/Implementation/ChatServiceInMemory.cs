using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    public class ChatServiceInMemory : IChatService
    {
        private IList<ChatUser> Admins;
        private IList<ChatRoom> ChatRooms;
        private int maxUsers;
        private int maxAdmins;

        public ChatServiceInMemory()
        {
            Admins = new List<ChatUser>();
            ChatRooms = new List<ChatRoom>();
        }
        
        public async Task AddChatUser(ChatUser user)
        {
            if(user.SecurityLevel==2)
            {
                Admins.Add(user);
            }else if(user.SecurityLevel==1)
            {
                ChatRoom created = new ChatRoom();
                created.Id = user.RoomId;
                created.Customer = user;
                ChatRooms.Add(created);
            }
            
        }

        public async Task<ChatRoom> GetRoom(long userId,bool isAdmin,string connectionId)
        {
            ChatRoom find = ChatRooms.FirstOrDefault(r => r.Customer.Id.Equals(userId));
            if(find!=null)
            {
                find.Customer.ConnectionId = connectionId;
                return find;
            }
            find = ChatRooms.FirstOrDefault(r => r.Admin.Id.Equals(userId));
            if(find!=null)
            {
                return find;
            }
            if(isAdmin)
            {
                ChatUser admin = new ChatUser(connectionId);
                admin.Status = 1;
                admin.Id = userId;
                Admins.Add(admin);
                return await NextInQueue(connectionId);
            }
            else
            {
                ChatRoom created = new ChatRoom();
                created.Id = connectionId;
                created.Customer = new ChatUser(connectionId);
                created.Customer.Status = 1;
                created.Customer.Id = userId;
                ChatRooms.Add(created);
                return created;
            }

            return null;
        }

        public async Task RemoveUser(string connectionId)
        {
            ChatRoom find = null;
            try
            {
                find = ChatRooms.First(r => r.Id == connectionId);
                
            }
            catch (Exception e)
            {
                ChatUser admin =  Admins.FirstOrDefault(u => u.ConnectionId.Equals(connectionId));
                ChatRoom room = ChatRooms.FirstOrDefault(r => r.Admin.ConnectionId.Equals(connectionId));
                if(room!=null)
                {
                    room.RoomStatus = 1;
                    room.Admin = new ChatUser("none");
                }

                if (admin != null)
                {
                    Admins.Remove(admin);
                }
                
                return;
            }

            find.Customer.ConnectionId = "none";
        }
        public async Task ChangeUserStatus(string connectionId, int status)
        {
            ChatUser user = await GetUser(connectionId);
            if(user!=null)
            {
                user.Status = status;
            }
        }

        public async Task<bool> IsAdmin(string connectionId)
        {
            return Admins.FirstOrDefault(u => u.ConnectionId.Equals(connectionId)) != null;
        }
        

        public async Task<IList<ChatRoom>> GetChatRooms()
        {
            return ChatRooms;
        }
        

        public async Task<string> GetUpdates(string connectionId)
        {
            var room = ChatRooms.FirstOrDefault(u => u.Customer.ConnectionId.Equals(connectionId));
            if(room!=null)
            {
                return "You are number " +(ChatRooms.Count-ChatRooms.IndexOf(room))+" in the queue";
            }

            return "You are not connected to a chat room";
        }

        public async Task<ChatUser> GetUser(string connectionId)
        {
            Console.WriteLine("find: "+connectionId);
            ChatUser find = ChatRooms.FirstOrDefault(r => r.Customer.ConnectionId.Equals(connectionId))?.Customer;
            if(find!=null)
            {
                return find;
            } 
            find = Admins.FirstOrDefault(r => r.ConnectionId.Equals(connectionId));
            if(find!=null)
            {
                return find;
            }
            
            return null;
        }

        public async Task<ChatRoom> NextInQueue(string adminConnectionId)
        {
            ChatRoom toRemove = ChatRooms.FirstOrDefault(u => u.RoomStatus == 1);
            if(toRemove!=null)
            {
                ChatUser admin = Admins.FirstOrDefault(u => u.ConnectionId.Equals(adminConnectionId));
                toRemove.Admin = admin;
            }
            return toRemove;
        }
        

        public async Task AddMessage(Message message, string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            room?.Messages.Add(message);
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

        public async Task<IList<ChatUser>> GetAdmins()
        {
            return Admins;
        }
        
    }
}