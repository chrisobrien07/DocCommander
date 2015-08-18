using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocCommander.Models;
using DocCommander.Logic;
using System.Web.Security;

namespace DocCommander.Controllers
{
    [Authorize]
    public class ADAuthController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //Authenticate against LDAP if the account has the IsLDAPAccount flag
            if (AccountRepos.GetIsLDAPAccount(model.UserName))
            {
                if (AuthenticateLDAP(model))
                {
                    if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return this.Redirect(returnUrl);
                    }
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return this.RedirectToAction("Index", "Home");
                }
            }
            this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            return this.View(model);
        }

        public bool AuthenticateLDAP(LoginModel model)
        {
            if (Membership.Providers["ADMembershipProvider"].ValidateUser(model.UserName, model.Password))
            {
                return true;
            }
            return false;
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

    }
}
