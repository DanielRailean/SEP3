using System;

namespace WebApp.Models
{
    public class Message
    {
        public string Body { get; set; }
        public bool IsAdminMessage { get; set; }
        public DateTime timestamp { get; set; }
    }
}