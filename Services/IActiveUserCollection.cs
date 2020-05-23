using System.Collections.Generic;
using signalr.Models;

namespace signalr.Services
{
    public interface IActiveUserCollection
    {
        List<User> ActiveUsers { get; }
        
        User NewUserLoggedIn(int id, string username, string connectionId);
        void UserLoggedOut(int id);
    }
}