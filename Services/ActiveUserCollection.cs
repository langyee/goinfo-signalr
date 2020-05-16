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

        public void NewUserLoggedIn(int id, string username)
        {
            if (ActiveUsers.Any(user => user.Id == id))
                return;

            ActiveUsers.Add(new User { Id = id, Username = username });
        }

        public void UserLoggedOut(int id)
        {
            var user = ActiveUsers.Find(user => user.Id == id);

            ActiveUsers.Remove(user);
        }
    }
}