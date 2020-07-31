using System;
using System.Globalization;

namespace signalr.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedAt { get; private set; }

        public User()
        {
            ConnectedAt = DateTime.Now;
        }
    }
}