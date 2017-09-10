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
        public ActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            IUserService userService = new FakeUserService();
            bool loginSuccess = userService.ValidateUser(model.UserName, model.Password);

            if (loginSuccess)
            {
                AuthenticationManager.SignIn(
                    new ClaimsIdentity(
                        new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, model.UserName) },
                        DefaultAuthenticationTypes.ApplicationCookie)
                    );

                if (returnUrl != null)
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

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}