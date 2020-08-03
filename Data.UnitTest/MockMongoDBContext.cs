using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitTest
{
    public class MockMongoDBContext
    {
        public static Mock<IMongoDBContext> GetMockDBContext()
        {
            return new Mock<IMongoDBContext>();
        }

        public static MongoDBContext GetTestDBContext()
        {
            var settings = new MongoSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestMessageStoreDb"
            };

            var context = new MongoDBContext(settings);

            return context;
        }
    }
}
