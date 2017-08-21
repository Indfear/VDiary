using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginController : SurfaceController
    {
        // GET: Login
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            if(Members.Login(model.Username, model.Password))
            {
                return Redirect("/");
            }

            ModelState.AddModelError("", "Invalid Login");

            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
            Members.Logout();
            return Redirect("/");
        }
    }
}