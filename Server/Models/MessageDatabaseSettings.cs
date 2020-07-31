using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalr.Models
{
    public class MessageDatabaseSettings : IMessageDatabaseSettings
    {
        public string MessagesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
