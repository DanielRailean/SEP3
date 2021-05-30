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
                OnlineUsers.Add(user);
                /*if(!IsUserConnected(userId))
                {
                }*/
            }
            // GetRoom(userId, isAdmin, connectionId);
        }
        public async Task<ChatUser> GetUser(string connectionId)
        {
            ChatUser find = OnlineUsers.FirstOrDefault(r => r.ConnectionId.Equals(connectionId));
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
        public async Task<ChatRoom> AskQuestion(string question,string connectionId)
        {
            ChatUser existingUser = await GetUser(connectionId);
            ChatRoom newRom = new ChatRoom();
            newRom.Id = connectionId;
            newRom.Question = question;
            newRom.Customer = existingUser;
            ChatRooms.Add(newRom);
            return newRom;
        }

        // public async Task AddChatUser(ChatUser user)
        // {
        //     if(user.SecurityLevel==2)
        //     {
        //         OnlineAdmins.Add(user);
        //     }else if(user.SecurityLevel==1)
        //     {
        //         ChatRoom created = new ChatRoom();
        //         created.Id = user.RoomId;
        //         created.Customer = user;
        //         ChatRooms.Add(created);
        //     }
        // }

        public async Task ChangeUserRoom(string userConnectionId, string roomId)
        {
            ChatUser user = await GetUser(userConnectionId);
            if (user != null)
            {
                user.CurrentRoom = roomId;
            }
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
        public async Task<ChatRoom> GetRoom(long userId,bool isAdmin,string connectionId)
        {
            // find room where customer id is the same
            ChatRoom find = ChatRooms.FirstOrDefault(r => r.Customer.Id.Equals(userId));
            //if found set the connection id and return it
            if(find!=null)
            {
                find.Customer.ConnectionId = connectionId;
                return find;
            }
            //find a room where admin id equals current id
            find = ChatRooms.FirstOrDefault(r => r.Admin.Id.Equals(userId));
            // if found  -> set connection id and return
            if(find!=null)
            {
                find.Admin.ConnectionId = connectionId;
                return find;
            }
            // if admin and not already in a room return empty
            if(isAdmin)
            {
                return null;
            }
            // else create a new room and add the online user there
            ChatRoom created = new ChatRoom();
                created.Id = connectionId;
                ChatUser user = OnlineUsers.FirstOrDefault(u => u.Id.Equals(userId));
                if (user != null)
                {
                    created.Customer = user;
                    ChatRooms.Add(created);
                }
                else
                {
                    await ConnectToChat(userId, isAdmin, connectionId, "Please reconnect");
                    ChatUser newUser = OnlineUsers.First(u => u.Id.Equals(userId));
                    created.Customer = newUser;
                    ChatRooms.Add(created);

                }
                return created;

        }

        public async Task<ChatRoom> GetRoom(string roomId)
        {
            ChatRoom find = ChatRooms.FirstOrDefault(r => r.Id.Equals(roomId));
            return find;
        }

        public async Task DisconnectUser(long userId)
        {
            //check if user wants to disconnect
            if (IsUserConnected(userId))
            { 
                // if online - remove from list
                ChatUser toRemove = OnlineUsers.First(u => u.Id==userId);

                    OnlineUsers.Remove(toRemove);
                    // if created a room before disconnecting - remove the room , set the admin to free
                    var find = ChatRooms.FirstOrDefault(r => r.Customer.Id.Equals(toRemove.Id));
                    if (find != null)
                    {
                        find.Admin.Status=4;
                        ChatRooms.Remove(find);
                    }
            }
            // else if admin wants to disconnect
            if (IsAdminConnected(userId))
            {
                // if online - remove from list
                ChatUser toRemove = OnlineAdmins.First(u => u.Id==userId);
                OnlineAdmins.Remove(toRemove);
                // check if is currently in a room -> if it is remove from there by assigning an empty user
                var find = ChatRooms.FirstOrDefault(r => r.Admin.Id.Equals(toRemove.Id));
                if (find != null)
                {
                    find.Admin = new ChatUser("none");
                    find.Status = 1;
                }
            }
            /*ChatRoom find = null;
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

            find.Customer.ConnectionId = "none";*/
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


        public async Task<string> GetUpdates(long userId, bool isAdmin)
        {
            ChatRoom room;
            if (isAdmin)
            {
                if (IsAdminConnected(userId))
                {
                    room = ChatRooms.FirstOrDefault(r => r.Admin.Id == userId);
                    if (room != null)
                    {
                        return ("You are now talking to " + room.Customer.FullName);
                    }

                    return "You are connected \n" + "There are " + ChatRooms.Count + " users left In queue";;

                }

                return "You are not connected";
            }
            else
            {
                if (IsUserConnected(userId))
                {
                    room = ChatRooms.FirstOrDefault(r => r.Customer.Id == userId);
                    if (room != null)
                    {
                        if (room.Admin.Status != 5)
                        {
                            return "You are number " + (ChatRooms.IndexOf(room)+1) + " in the queue";
                        }
                        return ("You are now talking to " + room.Admin.FullName);
                    }

                    return "You are connected";

                }

                return "You are not connected";
            }
                
            
            /*if (isAdmin)
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
                return "You are not connected";
                
            }
            else
            {
                if(IsUserConnected())
                room = ChatRooms.FirstOrDefault(u => u.Customer.ConnectionId.Equals(connectionId));
                if (room == null)
                {
                    return "Ask a question to start chatting";
                }
                if(room.Status==2)
                {
                    return "You are now talking to " + room.Admin.FullName;
                }
                return "You are number " + (ChatRooms.IndexOf(room)+1) + " in the queue";
            }*/
        }

        public bool IsUserConnected(long userId)
        {
            return OnlineUsers.FirstOrDefault(u => u.Id==userId) != null;
        }

        public bool IsAdminConnected(long userId)
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