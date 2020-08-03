using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public MongoDBContext(IMongoSettings configuration)
        {
            _mongoClient = new MongoClient(configuration.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return _db.GetCollection<T>(name);
        }
    }
}
