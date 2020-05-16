using System.Collections.Generic;
using signalr.Models;

namespace signalr.Services
{
    public interface IActiveUserCollection
    {
        List<User> ActiveUsers { get; }
        
        void NewUserLoggedIn(int id, string username);
        void UserLoggedOut(int id);
    }
}