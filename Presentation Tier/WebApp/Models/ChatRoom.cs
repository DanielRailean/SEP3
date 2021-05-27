using System.Collections.Generic;

namespace WebApp.Models
{
    public class ChatRoom
    {
        public string Id { get; set; }
        public ChatUser Admin { get; set; }
        public ChatUser Customer { get; set; }
        public IList<Message> Messages { get; set; }
}
}