using System;

namespace signalr.Models
{
    public class CustomMessage
    {
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }
}