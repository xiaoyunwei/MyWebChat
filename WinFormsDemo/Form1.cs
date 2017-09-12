using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Diagnostics;

namespace WinFormsDemo
{
    public partial class Form1 : Form
    {
        string baseUrl;
        IRestClient restClient;
        ChatMessage lastPrompMessage;

        public Form1()
        {
            InitializeComponent();

            baseUrl = ConfigurationManager.AppSettings["ChatWebBaseUrl"];
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl + "/";

            restClient = new RestClient(baseUrl);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            RestRequest request = new RestRequest("home/login", Method.POST);
            LoginViewModel model = new LoginViewModel();
            model.UserName = txtName.Text.Trim();
            model.Password = txtPassword.Text.Trim();
            request.AddBody(model);
            var response = await restClient.Execute(request);

            try
            {
                var messages = await this.GetUnreadMessage();
                MessageBox.Show("登陆成功！");

                tmrCheckMessage.Start();
            }
            catch
            {
                MessageBox.Show("登陆失败！");
            }
        }

        private async Task<IEnumerable<ChatMessage>> GetUnreadMessage()
        {
            RestRequest request = new RestRequest("api/message");
            var resp = await restClient.Execute<IEnumerable<ChatMessage>>(request);
            
            if(resp.Data.Any())
            {
                ChatMessage lastMessage = resp.Data.Last();

                if (lastPrompMessage == null || (lastPrompMessage.SenderName != lastMessage.SenderName && lastPrompMessage.Message != lastMessage.Message))
                {
                    notifyIcon.BalloonTipTitle = "您有新的消息";
                    notifyIcon.BalloonTipText = $"{lastMessage.SenderName}: {lastMessage.Message}";
                    notifyIcon.ShowBalloonTip(3000);
                }

                lastPrompMessage = lastMessage;
            }

            return resp.Data;
        }

        private async void tmrCheckMessage_Tick(object sender, EventArgs e)
        {
            await this.GetUnreadMessage();
        }

        private void GoChating(string userId = "")
        {
            string path = "chat";
            if(!string.IsNullOrEmpty(userId))
                path += $"?id={userId}";

            Process.Start($"{baseUrl}{path}");
        }

        private void btnShowMessage_Click(object sender, EventArgs e)
        {
            this.GoChating();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.GoChating(lastPrompMessage.SenderId);
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

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
