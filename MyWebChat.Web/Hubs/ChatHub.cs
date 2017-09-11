using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using MyWebChat.Web.Models;

namespace MyWebChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        // 此处仅为演示，实际请将以下数据存放在数据库中
        private readonly static List<string> onlineUsers = new List<string>();
        private readonly static List<ChatMessage> messages = new List<ChatMessage>();

        public static IEnumerable<string> OnlineUserNames
        {
            get
            {
                return onlineUsers;
            }
        }

        public static IEnumerable<ChatMessage> HistoryMessages
        {
            get
            {
                return messages;
            }
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            onlineUsers.Add(name);

            this.SendOfflineMessages(name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            onlineUsers.Remove(name);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            if (!onlineUsers.Contains(name))
            {
                onlineUsers.Add(name);
                this.SendOfflineMessages(name);
            }

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
            bool offline = ChatHub.onlineUsers.Contains(userId) ? false : true;

            ChatMessage cm = new ChatMessage()
            {
                SenderName = sender,
                ReceiverId = userId,
                Message = message,
                IsOffline = offline,
                SendTime = DateTime.Now
            };

            lock (ChatHub.messages)
            {
                ChatHub.messages.Add(cm);
            }

            if (!offline)
            {
                Clients.User(userId).receiveMessage(sender, message);
                cm.ReceiveTime = DateTime.Now;
            }
        }

        private void SendOfflineMessages(string userId)
        {
            var offlineMessages = ChatHub.messages.Where(p => p.IsOffline && p.ReceiverId == userId);
            foreach(var m in offlineMessages)
            {
                Clients.User(userId).receiveMessage(m.SenderName, m.Message);
                m.IsOffline = false;
            }
        }
    }
}