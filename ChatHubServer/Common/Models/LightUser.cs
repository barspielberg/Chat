using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// User model for sending to the client.
    /// in this model there is not sensitive data like password or id.
    /// IsConnected prop is for displaying if the user is connected to the chat right now.
    /// </summary>
    public class LightUser
    {
        public string Name { get; set; }
        public bool IsConnected { get; set; }
    }
}
