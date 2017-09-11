using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }

        public UserRole UserRole { get; set; }

        [Display(Name = "在线")]
        public bool IsOnline { get; set; }
    }
}