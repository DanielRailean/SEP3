using System.Collections.Generic;

namespace WebApp.Models
{
    /// <summary>
    /// Chat room model for admin-user communication.
    /// </summary>
    public class ChatRoom
    {
        public ChatRoom()
        {
            Admin = new ChatUser("none");
            Customer = new ChatUser("none");
            Status = 1;
            Messages = new List<Message>();
        }

        /// <summary>
        /// Id of the chat room.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Admin type of ChatUser.
        /// </summary>
        public ChatUser Admin { get; set; }

        /// <summary>
        /// Customer type of ChatUser.
        /// </summary>
        public ChatUser Customer { get; set; }

        /// <summary>
        /// List of previously conducted messages.
        /// </summary>
        public IList<Message> Messages { get; set; }

        /// <summary>
        /// 1 = user waiting for the admin 2= admin joined the chat 3 - admin was last that responded
        /// </summary>
        public int Status { get; set; }
    }
}