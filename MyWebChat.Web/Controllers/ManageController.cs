using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebChat.Web.Models;
using MyWebChat.Web.Services;
using MyWebChat.Web.Hubs;

namespace MyWebChat.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        IUserService userService = new FakeUserService();

        [HttpGet]
        public ActionResult Users()
        {
            string searchText = ViewBag.SearchText;
            var users = userService.GetUsers(searchText);
            foreach (var u in users.Where(p => ChatHub.OnlineUserNames.Contains(p.UserName)))
                u.IsOnline = true;

            return View(users);
        }
    }
}