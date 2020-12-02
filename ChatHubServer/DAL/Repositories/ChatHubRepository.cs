using Common.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ChatHubRepository : IChatHubRepository
    {
        private readonly ChatHubContext context;

        public ChatHubRepository(ChatHubContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Message>> GetMessagesAsync(int userNameId, int otherUserNameId)
        {
            return Task.Run(() => GetMessages(userNameId, otherUserNameId));
        }
        public IEnumerable<Message> GetMessages(int userNameId, int otherUserNameId)
        {
            return context.Messages.Where(m =>
            (m.SenderId == userNameId && m.ReceiverId == otherUserNameId) ||
            (m.SenderId == otherUserNameId && m.ReceiverId == userNameId))
                .OrderBy(m => m.Time);
        }

        public Task<bool> IsUserNameExistsAsync(string userName)
        {
            return Task.Run(() => IsUserNameExists(userName));
        }
        public bool IsUserNameExists(string userName)
        {
            return context.Users.FirstOrDefault(u => u.Name == userName) != null;
        }

        public Task<User> RegisterUserAsync(User user)
        {
            return Task.Run(() => RegisterUser(user));
        }
        public User RegisterUser(User user)
        {
            if (IsUserNameExists(user.Name))
                return null;
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
                return GetUserByName(user.Name);
            }
        }

        public Task SaveNewMessageAsync(Message message)
        {
            return Task.Run(() => SaveNewMessage(message));
        }
        public void SaveNewMessage(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
        }

        public Task<User> VerifyUserAsync(User user)
        {
            return Task.Run(() => VerifyUser(user));
        }
        public User VerifyUser(User user)
        {
            return context.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
        }

        public User GetUserById(int userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId);
        }
        public User GetUserByName(string userName)
        {
            return context.Users.FirstOrDefault(u => u.Name == userName);
        }

        public Message GetMessageById(int messageId)
        {
            return context.Messages.FirstOrDefault(m => m.Id == messageId);
        }

        public IEnumerable<User> GetUsers() => context.Users;

    }
}
