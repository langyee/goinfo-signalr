using System;

namespace signalr.Models 
{
    public class JournalMessage
    {
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
        public int Corpsno { get; set; }
        public string Corpname { get; set; }
        public DateTime Sdate { get; set; }
        public string Serno { get; set; }
    }
}