using MyWebChat.Web.Hubs;
using MyWebChat.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebChat.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public ActionResult Index(string id)
        {
            IUserService userService = new FakeUserService();
            var user = userService.GetUserByName(id);
            ViewBag.UserId = "";

            var sender = userService.GetUserByName(this.User.Identity.Name);

            // 获取在线用户列表
            List<SelectListItem> onlineUserList = new List<SelectListItem>();
            onlineUserList.Add(new SelectListItem() { Text = "请选择在线用户", Value = "" });

            var allUsers = userService.GetUsers();
            var onlineUsers = allUsers.Where(p => ChatHub.OnlineUserNames.Contains(p.UserName));
            foreach(var ou in onlineUsers)
            {
                if (ou.UserName == sender.UserName)
                    continue;

                SelectListItem item = new SelectListItem();
                item.Text = ou.DisplayName;
                item.Value = ou.UserName;
                if (user != null && ou.UserName == user.UserName)
                    item.Selected = true;

                onlineUserList.Add(item);
            }

            ViewData["OnlineUsers"] = onlineUserList;
            ViewBag.Sender = sender.DisplayName;
            if (sender.UserRole == Models.UserRole.管理员)
                ViewBag.CanBroadcast = true;
            else
                ViewBag.CanBroadcast = false;

            return View();
        }
    }
}