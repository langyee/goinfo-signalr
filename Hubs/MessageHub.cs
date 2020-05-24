using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using signalr.Services;
using signalr.Models;

namespace signalr.Hubs
{
    public class MessageHub : Hub
    {
        #region Property

        private IActiveUserCollection _activeUserCollection;

        public MessageHub(IActiveUserCollection activeUserCollection)
        {
            _activeUserCollection = activeUserCollection;
        }

        #endregion

        #region Action

        #region Plain Message

        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        #endregion

        #region User Connected

        public Task NotifyRandomUserLogin()
        {
            var rand = new Random();
            var randomId = rand.Next(100);
            var newUser = 
                _activeUserCollection.NewUserLoggedIn(
                    randomId, 
                    $"User {randomId}",
                    Context.ConnectionId
                );

            if (newUser != null)
                return Clients.All.SendAsync("NotifyNewUserLogin", _activeUserCollection.ActiveUsers);
            
            return null;
        }

        public Task NotifyNewUserLogin(int id, string username)
        {
            var newUser = _activeUserCollection.NewUserLoggedIn(id, username, Context.ConnectionId);

            if (newUser != null)
                return Clients.All.SendAsync("NotifyNewUserLogin", _activeUserCollection.ActiveUsers);

            return null;
        }

        #endregion

        #region User Disconnected

        public Task NotifyUserLogOut(int id)
        {
            _activeUserCollection.UserLoggedOut(id);

            return Clients.All.SendAsync("NotifyUserLogout", _activeUserCollection.ActiveUsers);
        }

        #endregion

        #region Share Message Object

        public Task SendCustomMessageToUser(
            string connectionId, 
            string sender, 
            string message)
        {
            // TODO: categorize message here and send the respective message
            // for example, if share journal, return SendAsync("ReceiveJournalMessage")
            // else return SendAsync("ReceiveLogMessage")

            var newMessage = MessageCategorizer.Evaluate(message);

            switch (newMessage)
            {
                case JournalMessage journalMessage:
                    return Clients.Client(connectionId).SendAsync("ReceiveJournalMessage", journalMessage);
                default:
                    var customMessage = newMessage as CustomMessage;
                    return Clients.Client(connectionId).SendAsync("ReceiveCustomMessage", customMessage);
            }
        }

        #endregion

        #region Default Life Cycle

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            var disconnectedUser = 
                _activeUserCollection.ActiveUsers.Find(user => user.ConnectionId == Context.ConnectionId);
            
            _activeUserCollection.ActiveUsers.Remove(disconnectedUser);

            Clients.All.SendAsync("UserDisconnected", _activeUserCollection.ActiveUsers);
            return base.OnDisconnectedAsync(ex);
        }

        #endregion

        #endregion
    }
}