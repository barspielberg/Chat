using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using ChatHubServer.Hubs;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatHubServer.Controllers
{
    /// <summary>
    /// Controller for managing all image related requests
    /// </summary>
    public class ImageUploaderController : Controller
    {
        private readonly IDataService dataService;
        private readonly IHubContext<ChatHub> hubContext;

        public ImageUploaderController(IDataService dataService, IHubContext<ChatHub> hubContext)
        {
            this.dataService = dataService;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Index(FormModel form)
        {
            LightMessage message = await dataService.SaveNewImgMessageAsync(form);

            string senderConnectionId = dataService.GetConnectionByUserName(message.Sender);
            string receiverConnectionId = dataService.GetConnectionByUserName(message.Receiver);
          
            await hubContext.Clients.Clients(senderConnectionId, receiverConnectionId).SendAsync("reciveMessage", message);

            return Ok();
        }

    }
}
