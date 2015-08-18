using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocCommander.Mailers;
using DocCommander.Models;
using DocCommander.Data;
using DocCommander.Logic;

namespace DocCommander.Controllers
{
    public class EmailController : Controller
    {
        private string siteName = "validationresource.com";
        private string siteUrl = "www.validationresources.com";


        public ActionResult SendWelcomeEmail(string username)
        {
            Account account = AccountRepos.Get(username);
            EmailWelcomeModel model = new EmailWelcomeModel()
            {
                SiteName = siteName,
                SiteUrl = siteUrl,
                FirstName = account.FirstName,
                LoginUrl = siteUrl + "/Account/login",
                SiteHelpUrl = siteUrl + "/Help/",
                ToEmail = account.Email,
                UserName = account.UserName
            };

            UserMailer mailer = new UserMailer();
            mailer.WelcomeMessage(model).Send();
            return RedirectToAction("RegistrationSuccess", "Account");
        }
        
        public ActionResult SendAccountConfirmationEmail(string username, string token)
        {
            Account account = AccountRepos.Get(username);
            EmailConfirmationModel model = new EmailConfirmationModel()
            {
                SiteName = siteName,
                SiteUrl = siteUrl,
                UserName = account.UserName,
                FirstName = account.FirstName,
                ToEmail = account.Email,
                ConfirmationToken = token,
                ConfirmationUrl = siteUrl + "/Account/ConfirmAccount?u=" + account.UserName + @"&t" + token                
            };

            UserMailer mailer = new UserMailer();
            mailer.ConfirmationTokenMessage(model).Send();
            return RedirectToAction("RegistrationSuccess","Account");
        }

        public ActionResult SendPasswordResetEmail(string username, string token)
        {
            Account account = AccountRepos.Get(username);
            EmailPasswordResetModel model = new EmailPasswordResetModel()
            {
                SiteName = siteName,
                SiteUrl = siteUrl,
                FirstName = account.FirstName,
                UserName = account.UserName,
                ToEmail = account.Email,
                PasswordResetToken = token,
                PasswordResetUrl = siteUrl + "/Account/ChangePassword?u=" + account.UserName + @"&t=" + token 
            };

            UserMailer mailer = new UserMailer();
            mailer.PasswordResetMessage(model).Send();
            return RedirectToAction("RequestChangePasswordSuccess", "Account");
        }

        public ActionResult SendPasswordChangeSuccessEmail(string username)
        {
            Account acc = AccountRepos.Get(username);

            UserMailer mailer = new UserMailer();
            mailer.PasswordChangeSuccessMessage(acc.UserName).Send();
            return RedirectToAction("RequestChangePasswordSuccess", "Account");
        }

        public ActionResult SendNotifyFailedLoginEmail(string username)
        {
            Account account = AccountRepos.Get(username);
            EmailNotifyFailedLoginAttemptModel model = new EmailNotifyFailedLoginAttemptModel()
            {
                SiteName = siteName,
                SiteUrl = siteUrl,
                ToEmail = account.Email,                
            };

            UserMailer mailer = new UserMailer();
            mailer.NotifyFailedLoginAttemptMessage(model).Send();
            return RedirectToAction("RequestChangePasswordSuccess", "Account");
        }

    }
}
