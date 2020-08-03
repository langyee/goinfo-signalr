using Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.UnitTest.UnitTests
{
    [TestClass]
    public class JournalMessageRepositoryTests
    {
        private Mock<IMongoCollection<JournalMessage>> _mockCollection = new Mock<IMongoCollection<JournalMessage>>();
        private Mock<IMongoDBContext> _mockDBContext = new MockMongoDBContext().GetMockDBContext();
        private JournalMessage _message = new JournalMessage
        {
            Corpname = "Test Name"
        };

        [TestMethod]
        public void JournalMessageRepository_CreateNewMessage_Valid_Success()
        {
            _mockCollection.Setup(op => op.InsertOneAsync(_message, null, default(CancellationToken)))
                .Returns(Task.CompletedTask);

            _mockDBContext.Setup(c => c.GetCollection<JournalMessage>(typeof(JournalMessage).Name)).Returns(_mockCollection.Object);

            var messageRepo = new JournalMessageRepository(_mockDBContext.Object);

            Task.Run(async () =>
            {
                await messageRepo.Create(_message);

                _mockCollection.Verify(c => c.InsertOneAsync(_message, null, default(CancellationToken)), Times.Once);
            });
        }

        [TestMethod]
        public void JournalMessageRepository_CreateNewMessage_Null_Message_Failure()
        {
            _message = null;

            var messageRepo = new JournalMessageRepository(_mockDBContext.Object);

            Task.Run(async () =>
            {
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => messageRepo.Create(_message));
            });
        }
    }
}
