using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApp.Models;

namespace WebApp.Data
{
    public class ChatHub : Hub
    {
        private IChatService ChatService;
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

        public async Task GoOnline(int userId, bool isAdmin , string name)
        {
            await ChatService.ConnectToChat(userId, isAdmin, Context.ConnectionId, name);
        }

        public async Task AskQuestion(string question)
        {
            ChatRoom justCreated = await ChatService.AskQuestion(question,Context.ConnectionId);
            await AddUserToHub(justCreated.Customer.ConnectionId, justCreated.Id);
        }
        public async Task ConnectToRoom(string roomId)
        {
            ChatRoom roomToConnectTo = await ChatService.ConnectToRoom(Context.ConnectionId, roomId);
            await AddUserToHub(Context.ConnectionId, roomToConnectTo.Id);
        }
        public async Task CloseChatRoom()
        {
            ChatUser currentUser = await ChatService.GetUser(Context.ConnectionId);
            await ChatService.RemoveRoom(currentUser.CurrentRoom);
        }
        public async Task ExitRoom()
        {
            ChatRoom toExit = await ChatService.ExitRoom(Context.ConnectionId);
            await RemoveUserFromHub(Context.ConnectionId, toExit.Id);
        }
        public async Task ReconnectToChat(int userId)
        {
            ChatRoom toReconnectTo = await ChatService.ReconnectToChat(userId,Context.ConnectionId);
            if (toReconnectTo != null)
            {
                await AddUserToHub(Context.ConnectionId, toReconnectTo.Id); 
            }

        }
        public async Task HelpNextUser()
        {
            ChatRoom nextRoom = await ChatService.NextInQueue(Context.ConnectionId);
            await AddUserToHub(Context.ConnectionId, nextRoom.Id);

        }
        
        public async Task SendMessage(string message)
        {
            ChatUser sender = await ChatService.GetUser(Context.ConnectionId);
            await SendToGroup(sender.CurrentRoom,sender.FullName, message);
            var newMessage = new Message {Body = message, Timestamp = DateTime.Now,Sender=sender.FullName,IsAdminMessage = sender.IsAdmin};
            await ChatService.AddMessage(newMessage, sender.CurrentRoom);
        }
        
        public async Task Disconnect(int userId)
        {
            ChatRoom disconnectFrom = await ChatService.DisconnectUser(userId);
            if (disconnectFrom != null)
            {
                await RemoveUserFromHub(Context.ConnectionId, disconnectFrom.Id);
            }
        }
        
        public async Task SendToGroup(string group,string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage",user,message);
        }

    }
}