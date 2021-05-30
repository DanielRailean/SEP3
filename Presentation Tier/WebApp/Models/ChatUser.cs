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

        public ChatUser(long id, string fullName, int securityLevel, int status, string connectionId, string roomId)
        {
            Id = id;
            FullName = fullName;
            SecurityLevel = securityLevel;
            Status = status;
            ConnectionId = connectionId;
            RoomId = roomId;
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
        // 1 - online, in user list
        // 2 - connected, in chat page
        // 3 - online, left page - room exists // NO WAY TO TRACK -> solution disposable
        
        //Only Admin states
        // 4 - online  in admin list
        // 5-  connected, in chat page
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
        public string RoomId { get; set; }
    }
}