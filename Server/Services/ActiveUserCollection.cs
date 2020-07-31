using System.Collections.Generic;
using System.Linq;
using signalr.Models;

namespace signalr.Services
{
    public class ActiveUserCollection : IActiveUserCollection
    {
        private List<User> _activeUsers = new List<User>();
        public List<User> ActiveUsers
        {
            get { return _activeUsers; }
        }

        public User NewUserLoggedIn(int id, string username, string connectionId)
        {
            var existingUser = ActiveUsers.Find(user => user.Id == id);
            if (existingUser != null) 
            {
                existingUser.ConnectionId = connectionId;
                return existingUser;
            }

            var newUser = new User 
            { 
                Id = id, 
                Username = username,
                ConnectionId = connectionId
            };
            ActiveUsers.Add(newUser);
            return newUser;
        }

        public void UserLoggedOut(int id)
        {
            var user = ActiveUsers.Find(user => user.Id == id);

            ActiveUsers.Remove(user);
        }
    }
}