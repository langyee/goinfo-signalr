using System;

namespace signalr.Models 
{
    public class JournalMessage
    {
        public int Corpsno { get; set; }
        public string Corpname { get; set; }
        public DateTime Sdate { get; set; }
        public string Serno { get; set; }
        public int Id { get; set; }
        public string Sender { get; set; }
    }
}