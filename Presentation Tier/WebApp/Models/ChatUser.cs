namespace WebApp.Models
{
    /// <summary>
    /// Chat user model.
    /// </summary>
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

        /// <summary>
        /// Id of the user.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// 1 = user, 2 = admin
        /// </summary>
        public int SecurityLevel { get; set; }
        
        /// <summary>
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
        /// </summary>

        public int Status { get; set; }
        
        /// <summary>
        /// Id for the connection.
        /// </summary>
        public string ConnectionId { get; set; }
        
        /// <summary>
        /// Id of the chat room.
        /// </summary>
        public string CurrentRoom { get; set; }
        /// <summary>
        /// Wether this user is admin
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}