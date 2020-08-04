using MongoDB.Driver;
using System.Collections.Generic;

namespace Data
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void DropDatabase(string databaseName);
        IEnumerable<string> ListDatabaseNames();
    }
}