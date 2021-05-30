using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApp.Models;

namespace WebApp.Data
{
    public class ChatHub : Hub
    {
        private IChatService ChatService;
        private int Time = 3;
        public ChatHub(IChatService chatService)
        {
            ChatService = chatService;
        }
        public async Task AddUserToHub(string userConnection, string groupName)
        {
            await Groups.AddToGroupAsync(userConnection, groupName);
        }
        public async Task RemoveUserFromHub(string userConnection, string groupName)
        {
            await Groups.RemoveFromGroupAsync(userConnection, groupName);
        }
        public async Task ConnectAdminHub(int userId, string name)
        {
            ChatRoom room = await ChatService.GetRoom(userId, true, Context.ConnectionId);
            if(room!=null)
            {
                await AddUserToHub(Context.ConnectionId, room.Id);
                room.Admin.FullName = name;
                await SendChatRoom(Context.ConnectionId, room.Id);
            }
            await SendConnectionId(Context.ConnectionId);
            Debug("Connect adminHub");
        }
        
        /*public override async Task OnConnectedAsync()
        {
            Debug();
        }*/
        public async Task GoOnline(int userId, bool isAdmin , string name)
        {
            await ChatService.ConnectToChat(userId, isAdmin, Context.ConnectionId, name);
            /*if (!Context.ConnectionId.Equals(connectionId))
            {
                await NotifyClient(Context.ConnectionId, "ClearLocalKeys", null);
                await SendConnectionId(Context.ConnectionId);
            } */
            /*if(isAdmin)
            {
                await SendChatRoom(Context.ConnectionId, "none");
            }*/
            Debug("Go online");
        }

        public async Task AskQuestion(string question)
        {
            ChatRoom justCreated = await ChatService.AskQuestion(question,Context.ConnectionId);
            await AddUserToHub(justCreated.Customer.ConnectionId, justCreated.Id);
            await ChatService.ChangeUserStatus(Context.ConnectionId, 2);
            await ChatService.ChangeUserRoom(Context.ConnectionId, justCreated.Id);
            Debug("ask question");
        }
        public async Task ConnectToRoom(string roomId)
        {
            ChatRoom roomToConnectTo = await ChatService.ConnectToRoom(Context.ConnectionId, roomId);
            await AddUserToHub(Context.ConnectionId, roomToConnectTo.Id);
            await SendToGroup(roomToConnectTo.Id,"admin", "connected");
            Debug("connect to room");
        }
        public async Task CloseChatRoom()
        {
            ChatUser currentUser = await ChatService.GetUser(Context.ConnectionId);
            await ChatService.RemoveRoom(currentUser.CurrentRoom);
            Debug("close room");
        }
        public async Task ExitRoom()
        {
            ChatRoom toExit = await ChatService.ExitRoom(Context.ConnectionId);
            await RemoveUserFromHub(Context.ConnectionId, toExit.Id);
            Debug("exit room");
        }
        public async Task ReconnectToChat(int userId)
        {
            ChatRoom toReconnectTo = await ChatService.ReconnectToChat(userId,Context.ConnectionId);
            if (toReconnectTo != null)
            {
                await AddUserToHub(Context.ConnectionId, toReconnectTo.Id);
                await SendToGroup(toReconnectTo.Id,"admin", "connected");
            }
            Debug("reconnect room");

        }
        public async Task HelpNextUser()
        {
            ChatRoom nextRoom = await ChatService.NextInQueue(Context.ConnectionId);
            await AddUserToHub(Context.ConnectionId, nextRoom.Id);
            await SendToGroup(nextRoom.Id,"admin", "connected");
            Debug("next room");
            /*foreach (var item in await ChatService.GetOnlineAdmins())
            {
                if (item.Status != 4) continue;
                if (!ChatService.GetChatRooms().Result.Any()) continue;
                var nextInQueue = await ChatService.NextInQueue(item.ConnectionId);
                if (nextInQueue == null) continue;
                await AddUserToHub(Context.ConnectionId, nextInQueue.Id);
                await AddUserToHub(nextInQueue.Customer.ConnectionId, nextInQueue.Id);
                nextInQueue.Admin.Status = 5;
                nextInQueue.Status = 2;
                nextInQueue.Admin.ConnectionId = Context.ConnectionId;
            }
            Debug("Match admin to user");*/
        }
        public async Task ConnectUserHub(int userId, string name)
        {
            ChatRoom room = await ChatService.GetRoom(userId, false, Context.ConnectionId);
            if (!Context.ConnectionId.Equals(room.Id))
            {
                await AddUserToHub(Context.ConnectionId, room.Id);
            }
            room.Customer.FullName = name;
            room.Customer.Status = 2;
            await SendConnectionId(Context.ConnectionId);
            await SendChatRoom(Context.ConnectionId, room.Id);
            Debug("Connect userhub");

        }
        
        public async Task SendMessage(string message)
        {
            ChatUser sender = await ChatService.GetUser(Context.ConnectionId);
            await SendToGroup(sender.CurrentRoom,sender.FullName, message);
            var newMessage = new Message {Body = message, Timestamp = DateTime.Now,Sender=sender.FullName,IsAdminMessage = sender.IsAdmin};
            await ChatService.AddMessage(newMessage, sender.CurrentRoom);
            Debug("SendMessage");
            /*var current = await ChatService.GetRoom(userId,isAdmin,Context.ConnectionId);
            if(current!=null)
            {
                if (message != null)
                {
                    if(isAdmin)
                    {
                        
                        newMessage.IsAdminMessage = true;
                        await ChatService.AddMessage(newMessage, current.Id);
                        await SendToGroup(current.Id,current.Admin.FullName, message);
                    }
                    else
                    {
                        var newMessage = new Message {Body = message, Timestamp = DateTime.Now,Sender=current.Admin.FullName};
                        newMessage.IsAdminMessage = false;
                        await ChatService.AddMessage(newMessage, current.Id);
                        await SendToGroup(current.Id,current.Customer.FullName, message);
                    }
                }
                await AddUserToHub(current.Customer.ConnectionId, current.Id);
                await AddUserToHub(current.Admin.ConnectionId, current.Id);

            }
        */
        }


        public async Task Reconnect(int userId,bool isAdmin)
        {
            var current = await ChatService.GetRoom(userId,isAdmin,Context.ConnectionId);
            if(current!=null)
            {
                await AddUserToHub(current.Customer.ConnectionId, current.Id);
                await AddUserToHub(current.Admin.ConnectionId, current.Id);
            }
            Debug("Reconnect");
        }
        public async Task SendConnectionId(string connectionId)
        {
            await NotifyClient(connectionId, "SetConnection", connectionId);
        }

        public async Task SendChatRoom(string connectionId, string chatRoomId)
        {
            await NotifyClient(connectionId, "SetChatRoom", chatRoomId);
        }


        public async Task Disconnect()
        {
            ChatRoom disconnectFrom = await ChatService.DisconnectUser(Context.ConnectionId);
            if (disconnectFrom != null)
            {
                await RemoveUserFromHub(Context.ConnectionId, disconnectFrom.Id);
            }
            Debug("Disconnect");
        }
        /*public override async Task OnDisconnectedAsync(Exception exception)
        {
 
            
            
        }*/
        
        

        public async Task GetUpdates(long userId,bool isAdmin)
        {
            var update = await ChatService.GetUpdates(userId,isAdmin);
            await NotifyClient(Context.ConnectionId, "Notify",update);
        }
        
        public async Task NotifyClient(string clientConnectionId,string method, string message)
        {
            await Clients.Client(clientConnectionId).SendAsync(method,message);
        }

        public async Task SendToGroup(string group,string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage",user,message);
        }

        private void Debug(string codePart)
        {
            Console.WriteLine("start" +codePart);
            Console.WriteLine();
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(ChatService.GetOnlineAdmins()));
            Console.WriteLine();
            Console.WriteLine("Online Users ");
            Console.WriteLine(JsonSerializer.Serialize(ChatService.GetOnlineUsers()));
            Console.WriteLine();
            Console.WriteLine("Rooms"+JsonSerializer.Serialize(ChatService.GetChatRooms()));
            Console.WriteLine();
            Console.WriteLine("end "+codePart);
        }
        
        /*public async Task NotifyServer(string message)
        {
            Console.WriteLine(message);
        }*/
        /*public async Task NotifyAllGroups(string message)
        {
            IList<ChatRoom> chatRooms = await ChatService.GetChatRooms();
            foreach (var item in chatRooms)
            {
                await NotifyClient(item.Customer.ConnectionId, message);
            }
        }
        */
        /*public async Task NotifyGroup(string group,string method, string message)
{
    await Clients.Group(group).SendAsync(method,message);
}*/
    }
}