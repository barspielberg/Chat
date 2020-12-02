using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// Message model for sending to the client.
    /// in this model there is not sensitive data like passwords or ids.
    /// </summary>
    public class LightMessage
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string ImgUrl { get; set; }
    }
}
