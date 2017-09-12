using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyWebChat.Web.Models;
using MyWebChat.Web.Services;
using Microsoft.Owin;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MyWebChat.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login(string usr = "", string pwd = "", string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(pwd))
            {
                string password = this.DecryptText(HttpUtility.UrlDecode(pwd));
                bool loginSuccess = this.LoginUser(usr, password);

                if(loginSuccess)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return Redirect("/");
                }                
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            bool loginSuccess = this.LoginUser(model.UserName, model.Password);
            if (loginSuccess)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/");
            }

            ModelState.AddModelError(string.Empty, "用户名或密码无效");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private bool LoginUser(string userName, string password)
        {
            IUserService userService = new FakeUserService();
            bool loginSuccess = userService.ValidateUser(userName, password);

            if (loginSuccess)
            {
                var usr = userService.GetUserByName(userName);

                AuthenticationManager.SignIn(
                    new ClaimsIdentity(
                        new[] {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                            new Claim("DisplayName", usr.DisplayName)
                        },
                        DefaultAuthenticationTypes.ApplicationCookie)
                    );
            }

            return loginSuccess;
        }

        /// <summary>
        /// 解密字符串，请使用实际解密方法替换
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        private string DecryptText(string encryptedText)
        {
            string decryptedText = encryptedText;
            return decryptedText;
        }
    }
}