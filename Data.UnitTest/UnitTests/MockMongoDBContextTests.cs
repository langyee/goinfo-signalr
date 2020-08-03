using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitTest.UnitTests
{
    [TestClass]
    public class MockMongoDBContextTests
    {
        private MockMongoDBContext _mockContext = new MockMongoDBContext();

        [TestMethod]
        public void MongoDBContext_Constructor_Success()
        {
            var context = _mockContext.GetDBContext();

            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void MongoDBContext_GetCollection_NameEmpty_Failure()
        {

            var context = _mockContext.GetDBContext();

            var myCollection = context.GetCollection<TestEntity>("");

            Assert.IsNull(myCollection);
        }

        [TestMethod]
        public void MongoDBContext_GetCollection_ValidName_Success()
        {
            var context = _mockContext.GetDBContext();

            var myCollection = context.GetCollection<TestEntity>("TestEntity");

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
