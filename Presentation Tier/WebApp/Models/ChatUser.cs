namespace WebApp.Models
{
    public class ChatUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public int SecurityLevel { get; set; }
        
        public bool IsInChat { get; set; }
        
        public string ConnectionId { get; set; }
        public string ChatRoom { get; set; }
    }
}