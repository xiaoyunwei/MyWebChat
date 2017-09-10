using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace MyWebChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="message">消息</param>
        public void Broadcast(string sender, string message)
        {
            Clients.All.receiveBroadcast(sender, message);
        }

        /// <summary>
        /// 发送消息给userId
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="userId">接收者UserName</param>
        /// <param name="message">消息</param>
        public void Send(string sender, string userId, string message)
        {
            Clients.User(userId).receiveMessage(sender, message);
        }
    }
}