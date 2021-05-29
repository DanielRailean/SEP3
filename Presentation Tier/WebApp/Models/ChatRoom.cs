using System.Collections.Generic;

namespace WebApp.Models
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            Admin = new ChatUser("none");
            Customer = new ChatUser("none");
            Status = 1;
            Messages = new List<Message>();
        }

        public string Id { get; set; }
        public ChatUser Admin { get; set; }
        public ChatUser Customer { get; set; }
        public IList<Message> Messages { get; set; }
        //1 = user waiting for the admin 2= admin joined the chat 
        public int Status { get; set; }
}
}