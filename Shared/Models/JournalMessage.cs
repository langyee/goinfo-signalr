using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class JournalMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public int Corpsno { get; set; }
        public string Corpname { get; set; }
        public DateTime Sdate { get; set; }
        public string Serno { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
        public int JournalId { get; set; }
        public string AdditaionalMessage { get; set; }
    }
}
