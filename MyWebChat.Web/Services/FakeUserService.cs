using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebChat.Web.Models;
using Microsoft.AspNet.SignalR;

namespace MyWebChat.Web.Services
{
    public class FakeUserService : IUserService, IUserIdProvider
    {
        List<User> users = new List<User>();

        public FakeUserService()
        {
            users.Add(new User() { UserName = "admin", DisplayName = "Administrator", UserRole = UserRole.管理员 });
            users.Add(new User() { UserName = "michael", DisplayName = "Michael Hu", UserRole = UserRole.管理员 });
            users.Add(new User() { UserName = "jack", DisplayName = "Jack Bauer", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "tom", DisplayName = "Tom Eddie", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "marti", DisplayName = "Martin Rock", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "peter", DisplayName = "Peter White", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "rose", DisplayName = "Rose Shen", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "vivian", DisplayName = "Vivian Bush", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "sunny", DisplayName = "Sunny Xia", UserRole = UserRole.普通用户 });
            users.Add(new User() { UserName = "linda", DisplayName = "Linda Wang", UserRole = UserRole.普通用户 });
        }

        public User GetUserByName(string userName)
        {
            return users.FirstOrDefault(p => p.UserName == userName);
        }

        public string GetUserId(IRequest request)
        {
            //HttpContext.Current.Session
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(string searchText)
        {
            return users.FindAll(p => p.UserName.Contains(searchText) || p.DisplayName.Contains(searchText));
        }

        public bool ValidateUser(string userName, string password)
        {
            return users.Any(p => p.UserName == userName && password == "abcd1234");
        }
    }
}