using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// Model for getting data from the client in more organized way
    /// </summary>
    public class FormModel
    {
        public IFormFile File { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}
