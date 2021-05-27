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
        private IChatService chatService;

        public ChatHub(IChatService userservice)
        {
            this.chatService = userservice;
        }

        public async Task SendMessage(int security, string message)
        {
            if (security == 1)
            {
                ChatUser sender = await chatService.GetUserByConnectionId(Context.ConnectionId);
                await Clients.Group(sender.ChatRoom).SendAsync("ReceiveMessage", sender.FirstName, message);
            }

            if (security == 2)
            {
                ChatUser sender = await chatService.GetAdminByConnectionId(Context.ConnectionId);
                await Clients.Group(sender.ChatRoom).SendAsync("ReceiveMessage", sender.FirstName, message);
            }

            Console.WriteLine("Message sent");
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

        public async Task getConnection(int securityLevel, string name)
        {
            if (securityLevel == 1)
            {
                ChatUser newChatUser = new ChatUser();
                newChatUser.SecurityLevel = 1;
                newChatUser.FirstName = name;
                newChatUser.ConnectionId = Context.ConnectionId;
                newChatUser.ChatRoom = "user_" + newChatUser.ConnectionId;
                await chatService.RegisterUser(newChatUser);
            }

            if (securityLevel == 2)
            {
                ChatUser newChatUser = new ChatUser();
                newChatUser.SecurityLevel = 2;
                newChatUser.FirstName = name;
                newChatUser.ConnectionId = Context.ConnectionId;
                newChatUser.ChatRoom = "admin_" + newChatUser.ConnectionId;
                await chatService.AddAdmin(newChatUser);
            }

            await match();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("USERS " + JsonSerializer.Serialize(chatService.GetAllUsers()));
            Console.WriteLine();
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(chatService.GetAllAdmins()));
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ChatUser sender = await chatService.GetUserByConnectionId(Context.ConnectionId);
            if (sender != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sender.ChatRoom);
                await chatService.RemoveUser(sender);
                await chatService.ChangeAdminStatus(sender.ChatRoom, false);
                await NotifyClient(sender.ConnectionId,
                    chatService.GetAllUsers().Result.Count + " users left in queue");
                await base.OnDisconnectedAsync(exception);
            }
            sender = await chatService.GetAdminByConnectionId(Context.ConnectionId);
            if (sender != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sender.ChatRoom);
                await chatService.RemoveAdmin(sender);
                await chatService.ChangeUserStatus(sender.ChatRoom, false);
                await base.OnDisconnectedAsync(exception);
            }
            await match();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("USERS " + JsonSerializer.Serialize(chatService.GetAllUsers()));
            Console.WriteLine();
            Console.WriteLine("ADMINS " + JsonSerializer.Serialize(chatService.GetAllAdmins()));
        }

        public async Task match()
        {
            foreach (var item in await chatService.GetAllAdmins())
            {
                if (!item.IsInChat)
                {
                    if (chatService.GetAllUsers().Result.Any())
                    {
                        ChatUser nextChatUser = await chatService.NextInQueue();
                        if(nextChatUser!=null)
                        {
                            await chatService.ChangeUserStatus(nextChatUser.ChatRoom, true);
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