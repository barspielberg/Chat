using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly Dictionary<string, User> connectionToUser = new Dictionary<string, User>();
        private readonly Dictionary<string, string> userNameToConnection = new Dictionary<string, string>();

        private readonly object padLock = new object();

        public void AddUserConnection(User user, string connection)
        {
            lock (padLock)
            {
                connectionToUser[connection] = user;
                userNameToConnection[user.Name] = connection;
            }
        }

        public bool RemoveConnection(string connection)
        {
            lock (padLock)
            {
                if (connectionToUser.Remove(connection, out User user) && userNameToConnection.Remove(user.Name))
                    return true;
                else return false;
            }
        }

        public string GetConnection(string userName)
        {
            lock (padLock)
            {
                if (userNameToConnection.TryGetValue(userName, out string connection))
                    return connection;
                else return null;
            }
        }

        public User GetUserByConnection(string connection)
        {
            lock (padLock)
            {
                if (connectionToUser.TryGetValue(connection, out User user))
                    return user;
                else return null;
            }
        }
    }
}
