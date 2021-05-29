namespace WebApp.Models
{
    public class ChatUser
    {
        public ChatUser(string connectionId)
        {
            ConnectionId = connectionId;
        }

        public ChatUser(long id, string fullName, int securityLevel, int status, string connectionId, string roomId)
        {
            Id = id;
            FullName = fullName;
            SecurityLevel = securityLevel;
            Status = status;
            ConnectionId = connectionId;
            RoomId = roomId;
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public int SecurityLevel { get; set; }
        
        //Only user states
        // 1 - online, in user list
        // 2 - connected,in room
        // 3 - online, left page - room exists
        
        //Only Admin states
        // 4 - online  in admin list
        // 5-  connected, in room

        public int Status { get; set; }
        
        public string ConnectionId { get; set; }
        public string RoomId { get; set; }
    }
}