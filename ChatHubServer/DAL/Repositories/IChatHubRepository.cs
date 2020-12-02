using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IChatHubRepository
    {
        /// <summary>
        /// Get all messages for specific chat between two users
        /// </summary>
        /// <param name="userNameId"></param>
        /// <param name="otherUserNameId"></param>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetMessagesAsync(int userNameId, int otherUserNameId);
        Task<bool> IsUserNameExistsAsync(string userName);
        /// <summary>
        /// If the username is available, creates new user with name and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The full user from the database, null if not available</returns>
        Task<User> RegisterUserAsync(User user);
        Task SaveNewMessageAsync(Message message);
        /// <summary>
        /// Verify the username and the password from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The full user from the database, null if one or both is incorrect</returns>
        Task<User> VerifyUserAsync(User user);
        User GetUserById(int userId);
        User GetUserByName(string userName);
        Message GetMessageById(int messageId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>All users in the database</returns>
        IEnumerable<User> GetUsers();
    }
}