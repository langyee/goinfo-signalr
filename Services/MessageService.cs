using MongoDB.Driver;
using signalr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalr.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<JournalMessage> _journalMessages;

        public MessageService(IMessageDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _journalMessages = database.GetCollection<JournalMessage>(settings.MessagesCollectionName);
        }

        public List<JournalMessage> Get() =>
            _journalMessages.Find(_ => true).ToList();

        public JournalMessage Create()
        {
            var newMessage = new JournalMessage
            {
                Corpname = "久泰營造",
                Corpsno = 1,
                Sdate = DateTime.Today,
                Sender = "Test",
                AdditaionalMessage = "This is a new message"
            };

            _journalMessages.InsertOne(newMessage);
            return newMessage;
        }
    }
}
