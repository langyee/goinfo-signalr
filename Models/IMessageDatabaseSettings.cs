namespace signalr.Models
{
    public interface IMessageDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MessagesCollectionName { get; set; }
    }
}