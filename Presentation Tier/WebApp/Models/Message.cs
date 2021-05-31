using System;

namespace WebApp.Models
{
    public class Message
    {
        public string Body { get; set; }
        public bool IsAdminMessage { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
    }
}