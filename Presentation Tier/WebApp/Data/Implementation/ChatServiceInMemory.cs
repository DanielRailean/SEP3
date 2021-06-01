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
        private IList<ChatUser> UsersFromRooms;
        private IList<ChatRoom> ChatRooms;
        private int maxUsers;
        private int maxAdmins;

        public ChatServiceInMemory()
        {
            OnlineAdmins = new List<ChatUser>();
            UsersFromRooms = new List<ChatUser>();
            ChatRooms = new List<ChatRoom>();
        }
        
        public async Task ConnectToChat(long userId, bool isAdmin, string connectionId, string name)
        {
            // Adding admin if not already connected
            if(isAdmin&& !IsAdminConnected(userId))
            {
                ChatUser admin = new ChatUser(connectionId);
                admin.Id = userId;
                admin.SecurityLevel = 2;
                admin.FullName = name;
                admin.Status = 4;
                admin.IsAdmin = true;
                OnlineAdmins.Add(admin);
                /*if(!IsAdminConnected(userId))
                {
                }*/
            }
            // Adding user if not already connected
            if(!isAdmin&&!IsUserConnected(userId))
            {
                ChatUser user = new ChatUser(connectionId);
                user.Id = userId;
                user.SecurityLevel = 1;
                user.FullName = name;
                user.Status = 1;
                user.IsAdmin = false;
                UsersFromRooms.Add(user);
                /*if(!IsUserConnected(userId))
                {
                }*/
            }
            // GetRoom(userId, isAdmin, connectionId);
        }
        public async Task<ChatUser> GetUser(string connectionId)
        {
            ChatUser find = UsersFromRooms.FirstOrDefault(r => r.ConnectionId.Equals(connectionId));
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

        public async Task<int> GetUserStatus(long userId)
        {
            ChatUser checkStatusUser = await GetUserById(userId);
            if (checkStatusUser != null)
            {
                return checkStatusUser.Status;
            }

            return 0;
        }

        public async Task<ChatUser> GetUserById(long userId)
        {
            ChatUser find = UsersFromRooms.FirstOrDefault(r => r.Id.Equals(userId));
            if(find!=null)
            {
                return find;
            } 
            find = OnlineAdmins.FirstOrDefault(r => r.Id.Equals(userId));
            if(find!=null)
            {
                return find;
            }
            
            return null;
        }

        public async Task<ChatRoom> AskQuestion(string question,string connectionId)
        {
            ChatUser existingUser = await GetUser(connectionId);
            ChatRoom newRom = new ChatRoom();
            newRom.Id = connectionId;
            newRom.Question = question;
            newRom.Customer = existingUser;
            newRom.Customer.Status = 2;
            newRom.Customer.CurrentRoom = newRom.Id;
            ChatRooms.Add(newRom);
            return newRom;
        }
        

        public async Task RemoveRoom(string roomId)
        {
            ChatRoom toRemove = await GetRoom(roomId);
            toRemove.Admin.Status = 4;
            toRemove.Customer.Status = 1;
            toRemove.Admin.CurrentRoom = "";
            toRemove.Customer.CurrentRoom = "";
            ChatRooms.Remove(toRemove);
        }
        public async Task<ChatRoom> ConnectToRoom(string userConnectionId, string roomId)
        {
            ChatUser existingUser = await GetUser(userConnectionId);
            ChatRoom toGetIn = await GetRoom(roomId);
            if (existingUser.IsAdmin)
            {
                existingUser.CurrentRoom = roomId;
                existingUser.Status = 5;
                toGetIn.Admin = existingUser;
            }
            else
            {
                existingUser.CurrentRoom = roomId;
                existingUser.Status = 2;
                toGetIn.Customer = existingUser;
            }

            return toGetIn;
        }
        public async Task<ChatRoom> ExitRoom(string userConnectionId)
        {
            ChatUser currentUser = await GetUser(userConnectionId);
            ChatRoom toExit = await GetRoom(currentUser.CurrentRoom);
            if (currentUser.IsAdmin)
            {
                toExit.Admin = new ChatUser("none");
                currentUser.Status = 4;
                currentUser.CurrentRoom = "";
            }
            else
            {
                toExit.Customer = new ChatUser("none");
                currentUser.Status = 1;
            }
            return toExit;
        }

        public async Task<ChatRoom> ReconnectToChat(int userId, string userConnectionId)
        {
            ChatUser beforeDisconnect = await GetUserById(userId);
            if (beforeDisconnect!=null)
            {
                ChatRoom roomBeforeDisconnect = await GetRoom(beforeDisconnect.CurrentRoom);
                if (roomBeforeDisconnect != null)
                {
                    beforeDisconnect.ConnectionId = userConnectionId;
                    if (beforeDisconnect.IsAdmin)
                    {
                        roomBeforeDisconnect.Admin.Status = 5;
                    }
                    else
                    {
                        roomBeforeDisconnect.Customer.Status = 2;
                    }
                }
                return roomBeforeDisconnect;
            }

            return null;
        }

        public async Task<ChatRoom> NextInQueue(string adminConnectionId)
        {
            ChatRoom nextRoom = ChatRooms.FirstOrDefault(u => u.Status == 1);
            ChatUser adminRequestingNextUser = await GetUser(adminConnectionId);
            adminRequestingNextUser.Status = 5;
            adminRequestingNextUser.CurrentRoom = nextRoom.Id;
            nextRoom.Admin = adminRequestingNextUser;
            return nextRoom;
        }

        public async Task<ChatRoom> GetRoom(string roomId)
        {
            ChatRoom find = ChatRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            return find;
        }

        public async Task<ChatRoom> DisconnectUser(int userId)
        {
            ChatUser userToDisconnect = await GetUserById(userId);
            ChatRoom roomToDisconnectFrom = await GetRoom(userToDisconnect.CurrentRoom);
            if (roomToDisconnectFrom != null)
            {
                if (userToDisconnect.IsAdmin)
                {
                    roomToDisconnectFrom.Admin.ConnectionId = "";
                    roomToDisconnectFrom.Admin.Status = 6;
                }
                else
                {
                    roomToDisconnectFrom.Customer.ConnectionId = "";
                    roomToDisconnectFrom.Customer.Status = 3;
                }
            }
            else
            {
                if (userToDisconnect.IsAdmin)
                {
                    OnlineAdmins.Remove(userToDisconnect);
                }
                else
                {
                    UsersFromRooms.Remove(userToDisconnect);
                }
            }

            return roomToDisconnectFrom;
        }
        
        
        public async Task<IList<ChatRoom>> GetChatRooms()
        {
            return ChatRooms;
        }
        

        public bool IsUserConnected(long userId)
        {
            return UsersFromRooms.FirstOrDefault(u => u.Id==userId) != null;
        }

        public bool IsAdminConnected(long userId)
        {
            return OnlineAdmins.FirstOrDefault(u => u.Id==userId) != null;
        }
        
        

        public async Task AddMessage(Message message, string roomId)
        {
            ChatRoom room = ChatRooms.FirstOrDefault(u=>u.Id.Equals(roomId));
            room?.Messages.Add(message);
        }
        
        
    }
}