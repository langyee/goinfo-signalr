﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitTest
{
    public class MockMongoDBContext
    {
        private Mock<IOptions<MongoSettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;


        public MockMongoDBContext()
        {
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }

        public MongoDBContext GetDBContext()
        {
            var settings = new MongoSettings()
            {
                ConnectionString = "mongodb://testDb",
                DatabaseName = "TestMessageStoreDb"
            };

            _mockOptions.Setup(s => s.Value).Returns(settings);
            _mockClient.Setup(c =>
                c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                .Returns(_mockDB.Object);

            var context = new MongoDBContext(_mockOptions.Object.Value);

            return context;
        }
    }
}
