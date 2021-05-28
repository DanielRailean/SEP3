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
        public ChatHub(IChatService UserService)
        {
            this.ChatService = UserService;
        }
        
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            NotifyAllGroups("update");
        }
        
        public async Task SendMessage(int security, string message)
        {
            if (security == 1)
            {
                ChatUser sender = await ChatService.GetUserByConnectionId(Context.ConnectionId);
                await Clients.Group(sender.ChatRoom).SendAsync("ReceiveMessage", sender.FirstName, message);
                Message msg = new Message();
                msg.timestamp = DateTime.Now;
                msg.Body = message;
                msg.IsAdminMessage = false;
                await ChatService.AddMessage(msg, sender.ChatRoom);
            }

            if (security == 2)
            {
                ChatUser sender = await ChatService.GetAdminByConnectionId(Context.ConnectionId);
                await Clients.Group(sender.ChatRoom).SendAsync("ReceiveMessage", sender.FirstName, message);
                Message msg = new Message();
                msg.timestamp = DateTime.Now;
                msg.Body = message;
                msg.IsAdminMessage = true;
                await ChatService.AddMessage(msg, sender.ChatRoom);
            }

            Console.WriteLine("Message sent");
            print();
        }

        public async Task GetUpdates()
        {
            string update = await ChatService.GetUpdates(Context.ConnectionId);
            await NotifyClient(Context.ConnectionId, update);
        }

        public async Task NotifyGroup(string group, string message)
        {
            await Clients.Group(group).SendAsync("Notify",message);
        }
        
        public async Task NotifyServer(string message)
        {
            Console.WriteLine(message);
        }
        public async Task NotifyClient(string clientConnectionId, string message)
        {
            await Clients.Client(clientConnectionId).SendAsync("Notify",message);
        }
        
        public async Task NotifyAllGroups(string message)
        {
            IList<ChatRoom> chatRooms = await ChatService.GetChatRooms();
            foreach (var item in chatRooms)
            {
                await NotifyClient(item.Customer.ConnectionId, message);
            }
        }

        public async Task GetConnection(int securityLevel, string name)
        {
            ChatUser newChatUser = new ChatUser();
            newChatUser.FirstName = name;
            newChatUser.ConnectionId = Context.ConnectionId;
            if (securityLevel == 1)
            {
                newChatUser.ChatRoom = "user_" + newChatUser.ConnectionId;
                newChatUser.SecurityLevel = 1;
                await ChatService.RegisterUser(newChatUser);

            }
            if (securityLevel == 2)
            {
                newChatUser.ChatRoom = "admin_" + newChatUser.ConnectionId;
                newChatUser.SecurityLevel = 2;
                await ChatService.AddAdmin(newChatUser);
                await NotifyClient(Context.ConnectionId, "You are now connected as admin with name "+ newChatUser.FirstName);

            }
            print();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ChatUser sender = await ChatService.GetUserByConnectionId(Context.ConnectionId);
            if (sender != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sender.ChatRoom);
                await ChatService.RemoveUser(sender);
                await ChatService.ChangeAdminStatus(sender.ChatRoom, false);
                await NotifyClient(sender.ConnectionId,
                    ChatService.GetAllUsers().Result.Count + " users left in queue");
                await base.OnDisconnectedAsync(exception);
            }
            sender = await ChatService.GetAdminByConnectionId(Context.ConnectionId);
            if (sender != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sender.ChatRoom);
                await ChatService.RemoveAdmin(sender);
                await ChatService.ChangeUserStatus(sender.ChatRoom, false);
                await base.OnDisconnectedAsync(exception);
            }
            print();
        }
        
        public void print()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("USERS " + JsonSerializer.Serialize(ChatService.GetAllUsers()));
            Console.WriteLine();
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(ChatService.GetAllAdmins()));
            Console.WriteLine();
            Console.WriteLine("Rooms"+JsonSerializer.Serialize(ChatService.GetChatRooms()));
        }
        public async Task Match()
        {
            foreach (var item in await ChatService.GetAllAdmins())
            {
                if (!item.IsInChat)
                {
                    if (ChatService.GetAllUsers().Result.Any())
                    {
                        ChatUser nextChatUser = await ChatService.NextInQueue(item.ConnectionId);
                        if(nextChatUser!=null)
                        {
                            await ChatService.ChangeUserStatus(nextChatUser.ChatRoom, true);
                            await Groups.AddToGroupAsync(nextChatUser.ConnectionId, nextChatUser.ChatRoom);
                            await Groups.AddToGroupAsync(item.ConnectionId, nextChatUser.ChatRoom);
                            item.ChatRoom = nextChatUser.ChatRoom;
                            item.IsInChat = true;
                            await NotifyClient(nextChatUser.ConnectionId, "You are now talking to " + item.FirstName);
                            await NotifyClient(item.ConnectionId, "You are now talking to " + nextChatUser.FirstName);

                        }
                    }
                }
            }
        }
    }
}