using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebChat.Web.Models
{
    public enum UserRole
    {
        普通用户 = 1,
        管理员
    }

    public class User
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public UserRole UserRole { get; set; }
    }
}