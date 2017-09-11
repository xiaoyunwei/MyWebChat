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

namespace WinFormsDemo
{
    public partial class Form1 : Form
    {
        string baseUrl;
        IRestClient restClient;

        public Form1()
        {
            InitializeComponent();

            baseUrl = ConfigurationManager.AppSettings["ChatWebBaseUrl"];
            restClient = new RestClient(baseUrl);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            RestRequest request = new RestRequest("home/login", Method.POST);
            LoginViewModel model = new LoginViewModel();
            model.UserName = txtName.Text.Trim();
            model.Password = txtPassword.Text.Trim();
            request.AddBody(model);
            //request.AddParameter("model", model, ParameterType.RequestBody);
            var task = restClient.Execute<Object>(request);
            task.Wait();
            if(task.Result.Data==null)
            {
                MessageBox.Show("ok");
            }
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class ChatMessage
    {
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public bool IsOffline { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime? ReceiveTime { get; set; }
    }
}
