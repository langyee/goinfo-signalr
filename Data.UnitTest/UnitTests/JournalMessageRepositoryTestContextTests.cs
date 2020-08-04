using Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitTest.UnitTests
{
    [TestClass]
    public class JournalMessageRepositoryTestContextTests
    {
        private MongoDBContext _testContext;
        private readonly string TESTDBNAME = "TestMessageStoreDb";

        [TestInitialize]
        public void testInit()
        {
            _testContext = MockMongoDBContext.GetTestDBContext();
            var messageRepo = new JournalMessageRepository(_testContext);
            var message = new JournalMessage()
            {
                Corpname = "Test Name"
            };

            Task.Run(async () =>
            {
                await messageRepo.Create(message);
            }).GetAwaiter().GetResult();
        }

        //[TestCleanup]
        //public void testCleanup()
        //{
        //    _testContext.DropDatabase(TESTDBNAME);
        //}

        [TestMethod]
        public void TestDBName_Exists()
        {
            var dbNameList = _testContext.ListDatabaseNames();
            var result = dbNameList.Contains(TESTDBNAME);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void JournalMessageRepository_Create_Valid_NewMessage_In_DB()
        {
            var messageRepo = new JournalMessageRepository(_testContext);
            var message = new JournalMessage()
            {
                Corpname = "Corp A"
            };

            Task.Run(async () =>
            {
                await messageRepo.Create(message);

                Assert.IsTrue(true);
            }).GetAwaiter().GetResult();


        }
    }
}
