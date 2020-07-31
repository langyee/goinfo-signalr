using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class MongoMessageDBContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        //public MongoMessageDBContext(IOptions<MongoSettings> configuration)
        //{

        //}
    }
}
