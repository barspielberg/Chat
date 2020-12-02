using Common.Models;
using System.Collections.Generic;

namespace BL.Services
{
    /// <summary>
    /// Service for managing key value pairs between users and their current connection id
    /// </summary>
    public interface IUserConnectionService
    {
        void AddUserConnection(User user, string connection);
        string GetConnection(string userName);
        User GetUserByConnection(string connection);
        bool RemoveConnection(string connection);
     
    }
}