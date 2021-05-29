﻿namespace WebApp.Models
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
        
        
        // 1 - Connected, not in room
        // 2- connected,in room
        // 3 - disconnected - room exists
        // 4 - connected not in admin list
        // 5-disconnected , in admin list
        // 6-connected- in admin list
        public int Status { get; set; }
        
        public string ConnectionId { get; set; }
        public string RoomId { get; set; }
    }
}