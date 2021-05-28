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
            ChatUser user = new ChatUser(userId,name,2,1,Context.ConnectionId,"admin_"+Context.ConnectionId);
            await ChatService.AddChatUser(user);
            await NotifyClient(Context.ConnectionId, "You are now connected as admin with name "+ user.FirstName);
            Debug("Connect adminHub");
        }
        public async Task ConnectUserHub(int userId, string name)
        {
            ChatUser user = new ChatUser(userId,name,1,1,Context.ConnectionId,"room_"+Context.ConnectionId);
            await ChatService.AddChatUser(user);
            await NotifyClient(Context.ConnectionId, "You are now connected as admin with name "+ user.FirstName);
            Debug("Connect userhub");

        }
        public async Task SendMessage(string message)
        {
            var current = await ChatService.GetRoom(Context.ConnectionId);
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
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ChatRoom room = await ChatService.GetRoom(Context.ConnectionId);
            if(room!=null)
            {
                await ChatService.RemoveUser(Context.ConnectionId);
            }
            
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
            await NotifyClient(Context.ConnectionId, update);
        }

        public async Task NotifyGroup(string group, string message)
        {
            await Clients.Group(group).SendAsync("Notify",message);
        }
        public async Task SendToGroup(string group,string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage",user,message);
        }
        
        /*public async Task NotifyServer(string message)
        {
            Console.WriteLine(message);
        }*/
        
        public async Task NotifyClient(string clientConnectionId, string message)
        {
            await Clients.Client(clientConnectionId).SendAsync("Notify",message);
        }
        
        /*public async Task NotifyAllGroups(string message)
        {
            IList<ChatRoom> chatRooms = await ChatService.GetChatRooms();
            foreach (var item in chatRooms)
            {
                await NotifyClient(item.Customer.ConnectionId, message);
            }
        }
        */
        
        
        
        private void Debug(string codePart)
        {
            Console.WriteLine(codePart);
            Console.WriteLine();
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(ChatService.GetAdmins()));
            Console.WriteLine();
            Console.WriteLine("Rooms"+JsonSerializer.Serialize(ChatService.GetChatRooms()));
            Console.WriteLine();
            Console.WriteLine(codePart);
        }
    }
}