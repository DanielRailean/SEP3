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
        public async Task ConnectAdminHub(int userId, string name)
        {
            ChatRoom room = await ChatService.GetRoom(userId, true, Context.ConnectionId);
            if (!room.Id.Equals(Context.ConnectionId))
            {
                await AddUserToHub(Context.ConnectionId, room.Id);
            }
            await NotifyClient(Context.ConnectionId, "Notify","You are now connected ");
            await SendConnectionId(Context.ConnectionId);
            Debug("Connect adminHub");
        }
        
        public async Task ConnectUserHub(int userId, string name)
        {
            ChatRoom room = await ChatService.GetRoom(userId, false, Context.ConnectionId);
            if (!room.Id.Equals(Context.ConnectionId))
            {
                await AddUserToHub(Context.ConnectionId, room.Id);
            }
            await NotifyClient(Context.ConnectionId, "Notify","You are now connected ");
            await SendConnectionId(Context.ConnectionId);
            Debug("Connect userhub");

        }
        
        public async Task SendMessage(int userId,bool isAdmin,string message)
        {
            var current = await ChatService.GetRoom(userId,isAdmin,Context.ConnectionId);
            Console.WriteLine("current :"+JsonSerializer.Serialize(current));
            if(current!=null)
            {
                var newMessage = new Message {Body = message, timestamp = DateTime.Now};
                await ChatService.AddMessage(newMessage, current.Id);
                if(await ChatService.IsAdmin(Context.ConnectionId))
                {
                    newMessage.IsAdminMessage = true;
                    await SendToGroup(current.Id,current.Admin.FirstName, message);
                }
                else
                {
                    await SendToGroup(current.Id,current.Customer.FirstName, message);
                }

            }
            Debug("SendMessage");
        }

        public async Task SendConnectionId(string connectionId)
        {
            await NotifyClient(connectionId, "SetConnection", connectionId);
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
 
            await ChatService.RemoveUser(Context.ConnectionId);
            
            
            Debug("Disconnect");
            
        }
        
        public async Task Match()
        {
            foreach (var item in await ChatService.GetAdmins())
            {
                if (item.Status != 1) continue;
                if (!ChatService.GetChatRooms().Result.Any()) continue;
                var nextInQueue = await ChatService.NextInQueue(item.ConnectionId);
                if (nextInQueue == null) continue;
                await AddUserToHub(item.ConnectionId, nextInQueue.Id);
                await AddUserToHub(nextInQueue.Customer.ConnectionId, nextInQueue.Id);
                await ChatService.ChangeUserStatus(item.ConnectionId, 2);
                nextInQueue.Customer.Status = 2;
                nextInQueue.RoomStatus = 2;
            }
            Debug("Match admin to user");
        }
        
        public async Task AddUserToHub(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
        
        public async Task GetUpdates()
        {
            var update = await ChatService.GetUpdates(Context.ConnectionId);
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
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(ChatService.GetAdmins()));
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