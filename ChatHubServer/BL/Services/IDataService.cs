using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    /// <summary>
    /// Main service for managing all users and messages
    /// </summary>
    public interface IDataService
    {

        /// <summary>
        /// Creates a new message object and save it on the database
        /// </summary>
        /// <param name="receiverUserName"></param>
        /// <param name="text"></param>
        /// <param name="senderUserName"></param>
        /// <param name="imgUrl"></param>
        /// <returns>Light meassage for sending to the client</returns>
        Task<LightMessage> CreateMessageForUserAsync(string receiverUserName, string text, string senderUserName, string imgUrl = null);
        /// <summary>
        /// Get all messages for specific chat between two users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="otherUserName"></param>
        /// <returns></returns>
        Task<IEnumerable<LightMessage>> GetMessagesAsync(string userName, string otherUserName);
        Task<bool> IsUserNameExistsAsync(string userName);
        Task<bool> LogInUserAsync(User user, string connectionId);
        Task<bool> RegisterUserAsync(User user, string connectionId);
        void RemoveConnection(string connectionId);
        /// <summary>
        /// Save the img file on the server and Creates a new message object and save it on the database
        /// </summary>
        /// <param name="form"></param>
        /// <returns>Light meassage for sending to the client</returns>
        Task<LightMessage> SaveNewImgMessageAsync(FormModel form);
        string GetConnectionByUserName(string userName);
        IEnumerable<LightUser> GetUsers();
        User GetUserByConnection(string connection);

    }
}