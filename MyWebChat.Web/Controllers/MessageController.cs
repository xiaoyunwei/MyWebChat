using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebChat.Web.Models;
using MyWebChat.Web.Hubs;

namespace MyWebChat.Web.Controllers
{
    [Authorize]
    public class MessageController : ApiController
    {
        public IEnumerable<ChatMessage> Get()
        {
            string userId = this.User.Identity.Name;
            var result = ChatHub.HistoryMessages.Where(p => p.IsOffline && p.ReceiverId == userId);
            return result;
        }
    }
}
