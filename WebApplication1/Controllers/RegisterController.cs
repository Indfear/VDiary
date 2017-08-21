using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : SurfaceController
    {
        // GET: Register
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var memberService = Services.MemberService;

            if (memberService.GetByEmail(model.Email) != null)
            {
                ModelState.AddModelError("", "A member with that email already exists");
                return CurrentUmbracoPage();
            }

            var member = memberService.CreateMemberWithIdentity(model.Email, model.Email, model.Name, "siteMember");

            member.SetValue("bio", model.Biography);
            member.SetValue("avatar", model.Avatar);
            memberService.SavePassword(member, model.Password);
            Members.Login(model.Email, model.Password);

            memberService.Save(member);

            return Redirect("/");
        }
    }
}