using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    public class ChatServiceInMemory : IChatService
    {
        private IList<ChatUser> OnlineAdmins;
        private IList<ChatUser> OnlineUsers;
        private IList<ChatRoom> ChatRooms;
        private int maxUsers;
        private int maxAdmins;

        public ChatServiceInMemory()
        {
            OnlineAdmins = new List<ChatUser>();
            OnlineUsers = new List<ChatUser>();
            ChatRooms = new List<ChatRoom>();
        }

        public bool IsAdminConnected(long userId)
        {
            return OnlineAdmins.FirstOrDefault(u => u.Id==userId) != null;
        }

        public async Task AddChatUser(ChatUser user)
        {
            if(user.SecurityLevel==2)
            {
                OnlineAdmins.Add(user);
            }else if(user.SecurityLevel==1)
            {
                ChatRoom created = new ChatRoom();
                created.Id = user.RoomId;
                created.Customer = user;
                ChatRooms.Add(created);
            }
            
        }

        public async Task<ChatRoom> GetConnection(long userId,bool isAdmin,string connectionId,string name)
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
                return null;
            }

            ChatRoom created = new ChatRoom();
                created.Id = connectionId;
                created.Customer = new ChatUser(connectionId);
                created.Customer.Status = 1;
                created.Customer.Id = userId;
                created.Customer.FullName = name;
                ChatRooms.Add(created);
                return created;

                return null;
        }

        public async Task DisconnectUser(string connectionId)
        {
            ChatRoom find = null;
            try
            {
                find = ChatRooms.First(r => r.Id == connectionId);
                
            }
            catch (Exception e)
            {
                ChatUser admin =  OnlineAdmins.FirstOrDefault(u => u.ConnectionId.Equals(connectionId));
                ChatRoom room = ChatRooms.FirstOrDefault(r => r.Admin.ConnectionId.Equals(connectionId));
                if(room!=null)
                {
                    room.Status = 1;
                    room.Admin = new ChatUser("none");
                }

                if (admin != null)
                {
                    OnlineAdmins.Remove(admin);
                }
                
                return;
            }

            find.Customer.ConnectionId = "none";
        }

        public Task StopChat(string connectionId)
        {
            throw new NotImplementedException();
        }

        public Task GoOffline(long userId, bool isAdmin, string connectionId, string name)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeUserStatus(string connectionId, int status)
        {
            ChatUser user = await GetUser(connectionId);
            if(user!=null)
            {
                user.Status = status;
            }
        }

        public bool IsAdminOnline(string connectionId)
        {
            return OnlineAdmins.FirstOrDefault(u => u.ConnectionId.Equals(connectionId)) != null;
        }
        public bool IsUserOnline(string connectionId)
        {
            return OnlineUsers.FirstOrDefault(u => u.ConnectionId.Equals(connectionId)) != null;
        }


        public async Task<IList<ChatUser>> GetOnlineUsers()
        {
            return OnlineUsers;
        }

        public async Task<IList<ChatRoom>> GetChatRooms()
        {
            return ChatRooms;
        }
        

        public async Task<string> GetUpdates(string connectionId,bool isAdmin)
        {
            ChatRoom room;
            if (isAdmin)
            {
                if (IsAdminOnline(connectionId))
                {
                    if(ChatRooms.Any())
                    { 
                        room = ChatRooms.FirstOrDefault(u => u.Admin.ConnectionId.Equals(connectionId));
                        if (room==null)
                        {
                            return "There are " + ChatRooms.Count + " users left In queue";
                        }

                        return "You are now talking to " + room.Customer.FullName;
                    }

                    return "There are no more users to help";
                    
                }
                return "You are connected but not yet helping users, press Connect, then help next user to start";
                
            }
            else
            {
                room = ChatRooms.FirstOrDefault(u => u.Customer.ConnectionId.Equals(connectionId));
                if (room == null)
                {
                    return "You are online but not yet connected. Press connect to start";
                }
                if(room.Status==2)
                {
                    return "You are now talking to " + room.Admin.FullName;
                }
                return "You are number " + (ChatRooms.IndexOf(room)+1) + " in the queue";
            }
        }

        public async Task<ChatUser> GetUser(string connectionId)
        {
            ChatUser find = ChatRooms.FirstOrDefault(r => r.Customer.ConnectionId.Equals(connectionId))?.Customer;
            if(find!=null)
            {
                return find;
            } 
            find = OnlineAdmins.FirstOrDefault(r => r.ConnectionId.Equals(connectionId));
            if(find!=null)
            {
                return find;
            }
            
            return null;
        }

        public bool IsUserConnected(long userId)
        {
            return OnlineAdmins.FirstOrDefault(u => u.Id==userId) != null;
        }

        public async Task<ChatRoom> NextInQueue(string adminConnectionId)
        {
            ChatRoom toRemove = ChatRooms.FirstOrDefault(u => u.Status == 1);
            if(toRemove!=null)
            {
                ChatUser admin = OnlineAdmins.FirstOrDefault(u => u.ConnectionId.Equals(adminConnectionId));
                toRemove.Admin = admin;
            }
            return toRemove;
        }
        

        public async Task AddMessage(Message message, string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            room?.Messages.Add(message);
        }

        public async Task ConnectToChat(long userId, bool isAdmin, string connectionId, string name)
        {
            if(isAdmin)
            {
                ChatUser admin = new ChatUser(connectionId);
                admin.Id = userId;
                admin.SecurityLevel = 2;
                admin.FullName = name;
                admin.Status = 4;
                if(!IsAdminConnected(userId))
                {
                    OnlineAdmins.Add(admin);
                }
            }
            else
            {
                ChatUser user = new ChatUser(connectionId);
                user.Id = userId;
                user.SecurityLevel = 1;
                user.FullName = name;
                user.Status = 1;
                if(!IsUserConnected(userId))
                {
                    OnlineUsers.Add(user);
                }
                
            }
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

        public async Task<IList<ChatUser>> GetOnlineAdmins()
        {
            return OnlineAdmins;
        }
        
    }
}