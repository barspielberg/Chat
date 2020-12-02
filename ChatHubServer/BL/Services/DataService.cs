using Common.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DataService : IDataService
    {
        private readonly IUserConnectionService connectionService;
        private readonly IChatHubRepository repository;

        public DataService(IUserConnectionService connectionService, IChatHubRepository repository)
        {
            this.connectionService = connectionService;
            this.repository = repository;
        }

        public User GetUserByConnection(string connection)
            => connectionService.GetUserByConnection(connection);

        public async Task<LightMessage> CreateMessageForUserAsync(string receiverUserName, string text, string senderUserName, string imgUrl = null)
        {
            if (!await IsUserNameExistsAsync(receiverUserName) || !await IsUserNameExistsAsync(senderUserName)) return null;

            User sender = repository.GetUserByName(senderUserName);
            User receiver = repository.GetUserByName(receiverUserName);

            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Time = DateTime.Now,
                Text = text,
                ImgUrl = imgUrl
            };

            await repository.SaveNewMessageAsync(message);

            LightMessage lightMessage = new LightMessage
            {
                Sender = sender.Name,
                Receiver = receiver.Name,
                Text = text,
                Time = message.Time,
                ImgUrl = message.ImgUrl
            };
            return lightMessage;
        }

        public async Task<IEnumerable<LightMessage>> GetMessagesAsync(string userName, string otherUserName)
        {
            int userId = repository.GetUserByName(userName).Id;
            int otherUserId = repository.GetUserByName(otherUserName).Id;
            return (await repository.GetMessagesAsync(userId, otherUserId))
                .Select(m =>
                new LightMessage
                {
                    Sender = m.Sender.Name,
                    Receiver = m.Receiver.Name,
                    Text = m.Text,
                    Time = m.Time,
                    ImgUrl = m.ImgUrl
                });
        }

        public Task<bool> IsUserNameExistsAsync(string userName)
        {
            return repository.IsUserNameExistsAsync(userName);
        }

        public async Task<bool> RegisterUserAsync(User user, string connectionId)
        {
            var fullUser = await repository.RegisterUserAsync(user);
            if (fullUser != null)
            {
                connectionService.AddUserConnection(fullUser, connectionId);
                return true;
            }
            return false;
        }

        public void RemoveConnection(string connectionId)
            => connectionService.RemoveConnection(connectionId);


        public async Task<bool> LogInUserAsync(User user, string connectionId)
        {
            var fullUser = await repository.VerifyUserAsync(user);
            if (fullUser != null)
            {
                connectionService.AddUserConnection(fullUser, connectionId);
                return true;
            }
            return false;
        }

        public async Task<LightMessage> SaveNewImgMessageAsync(FormModel form)
        {
            var imgName = $"{Guid.NewGuid()} { form.File.FileName}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", imgName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                form.File.CopyTo(stream);
            }

            return await CreateMessageForUserAsync(form.Receiver, form.Text, form.Sender, $"Images/{imgName}");
        }

        public string GetConnectionByUserName(string userName)
            => connectionService.GetConnection(userName);


        public IEnumerable<LightUser> GetUsers()
        {
            return repository.GetUsers().Select(u =>
            {
                return new LightUser
                {
                    Name = u.Name,
                    IsConnected = connectionService.GetConnection(u.Name) != null
                };
            });
        }


    }
}
