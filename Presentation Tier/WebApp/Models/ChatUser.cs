namespace WebApp.Models
{
    public class ChatUser
    {
        public ChatUser(string connectionId)
        {
            ConnectionId = connectionId;
        }

        public ChatUser(long id, string fullName, int securityLevel, int status, string connectionId, string currentRoom)
        {
            Id = id;
            FullName = fullName;
            SecurityLevel = securityLevel;
            Status = status;
            ConnectionId = connectionId;
            currentRoom = currentRoom;
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public int SecurityLevel { get; set; }
        
        //Only user states
        // 0 - default user do not exist
        // 1 - online, in user list
        // 2 - online, in room
        // 3 - offline, left page - room exists // NO WAY TO TRACK -> solution disposable
        
        //Only Admin states
        // 0 - admin do not exist
        // 4 - online  in admin list
        // 5-  online, in room
        // 6- connected , left chat page


        public int Status { get; set; }
        
        public string ConnectionId { get; set; }
        public string CurrentRoom { get; set; }
        public bool IsAdmin { get; set; }
    }
}