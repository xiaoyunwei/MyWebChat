using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebChat.Web.Models;

namespace MyWebChat.Web.Services
{
    public interface IUserService
    {
        bool ValidateUser(string userName, string password);

        User GetUserByName(string userName);

        IEnumerable<User> GetUsers(string searchText);
    }
}
