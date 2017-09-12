using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebChat.Web.Models
{
    public class ChatMessage
    {
        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public bool IsOffline { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime? ReceiveTime { get; set; }
    }
}