using BL.Services;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatHubServer.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IDataService dataService;

        public ChatHub(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (await dataService.RegisterUserAsync(user, Context.ConnectionId))
            {
                SendUserListUpdated();
                return true;
            }
            else return false;
        }


        public async Task<bool> LoginUserAsync(User user)
        {
            if (await dataService.LogInUserAsync(user, Context.ConnectionId))
            {
                SendUserListUpdated();
                return true;
            }
            else return false;
        }


        public async Task<bool> SendMessageToUserAsync(string userName, string text)
        {
            User sender = dataService.GetUserByConnection(Context.ConnectionId);
            LightMessage message = await dataService.CreateMessageForUserAsync(userName, text, sender.Name);

            if (message == null) return false;

            await Clients.Caller.SendAsync("reciveMessage", message);

            string connectionId = dataService.GetConnectionByUserName(message.Receiver);
            if (connectionId != null)
                await Clients.Client(connectionId).SendAsync("reciveMessage", message);

            return true;
        }

        public async Task<bool> IsUserExistsAsync(string userName)
            => await dataService.IsUserNameExistsAsync(userName);

        public async Task<IEnumerable<LightMessage>> GetMessagesAsync(string userName, string otherUserName)
            => await dataService.GetMessagesAsync(userName, otherUserName);

        public IEnumerable<LightUser> GetUsers()
            => dataService.GetUsers();

        public override Task OnDisconnectedAsync(Exception exception)
        {
            dataService.RemoveConnection(Context.ConnectionId);
            SendUserListUpdated();
            return base.OnDisconnectedAsync(exception);
        }
   
        private async void SendUserListUpdated()
        {
            var lightUsers = dataService.GetUsers();
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UserListUpdated", lightUsers);
        }
    }
}
