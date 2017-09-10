using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;
using MyWebChat.Web.Services;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(MyWebChat.Web.Startup))]
namespace MyWebChat.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login/")
            });

            app.MapSignalR();
        }
    }
}