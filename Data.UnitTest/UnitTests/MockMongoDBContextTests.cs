using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitTest.UnitTests
{
    [TestClass]
    public class MockMongoDBContextTests
    {
        private MongoDBContext _mongoDBContext;

        private Mock<IOptions<MongoSettings>> _mockOptions;

        //private Mock<IMongoDatabase> _mockDB;

        //private Mock<IMongoClient> _mockClient;


        public MockMongoDBContextTests()
        {
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            //_mockDB = new Mock<IMongoDatabase>();
            //_mockClient = new Mock<IMongoClient>();
        }

        [TestInitialize]
        public void testInit()
        {
            var settings = new MongoSettings()
            {
                ConnectionString = "mongodb://testdb",
                DatabaseName = "TestMessageStoreDb"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            //_mockClient.Setup(c =>
            //    c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
            //    .Returns(_mockDB.Object);

            _mongoDBContext = new MongoDBContext(_mockOptions.Object.Value);
        }

        [TestMethod]
        public void MongoDBContext_Constructor_Success()
        {
            Assert.IsNotNull(_mongoDBContext);
        }

        [TestMethod]
        public void MongoDBContext_GetCollection_NameEmpty_Failure()
        {
            var myCollection = _mongoDBContext.GetCollection<TestEntity>("");

            Assert.IsNull(myCollection);
        }

        [TestMethod]
        public void MongoDBContext_GetCollection_ValidName_Success()
        {
            var myCollection = _mongoDBContext.GetCollection<TestEntity>("TestEntity");

            Assert.IsNotNull(myCollection);
        }

        public class TestEntity
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            public string Title { get; set; }
        }
    }
}
