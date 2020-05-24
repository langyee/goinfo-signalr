using System;
using signalr.Models;

namespace signalr.Hubs 
{
    public class MessageCategorizer
    {
        public static object Evaluate(string message)
        {
            var listOfMessages = message.Split("--");

            if (listOfMessages[0] == "journal")
            {
                var sender = listOfMessages[1];
                var corpsno = int.Parse(listOfMessages[2]);
                var corpname = listOfMessages[3];
                var sdate = Convert.ToDateTime(listOfMessages[4]);
                var serno = listOfMessages[5];

                return new JournalMessage 
                {
                    Sender = sender,
                    Timestamp = DateTime.Now,
                    Corpsno = corpsno,
                    Corpname = corpname,
                    Sdate = sdate, 
                    Serno = serno
                };
            }
            else
            {
                var sender = listOfMessages[1];
                var content = listOfMessages[2];
                return new CustomMessage 
                {
                    Sender = sender,
                    Timestamp = DateTime.Now,
                    Content = content
                };
            }
        }
    }
}