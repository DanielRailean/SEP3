using System;

namespace WebApp.Models
{
    /// <summary>
    /// Model for the chat messages.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Message body.
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        /// If the message was sent by the admin or not.
        /// </summary>
        public bool IsAdminMessage { get; set; }
        
        /// <summary>
        /// Name of the sender of the message.
        /// </summary>
        public string Sender { get; set; }
        
        /// <summary>
        /// Time stamp for each message.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}