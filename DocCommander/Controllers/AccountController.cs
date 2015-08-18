using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using DocCommander.Data;
using DocCommander.Models;
using DocCommander.Logic;
using DocCommander.Mailers;

namespace DocCommander.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
       
        #region login Methods
        /// <summary>
        /// Displays the login form
        /// </summary>
        /// <returns>Returns the login form view</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Processes the Login form. 
        /// Http post only.
        /// Checks the anitforgery token to protect against over posting attacks
        /// Notifies the user on password failure.
        /// Returns to Form display if username or password is incorrect.
        /// Locks the account after the max logins has been reached.
        /// Disables the account if max logins has been reached
        /// resets bad Logins to zero on successful login.
        /// 
        /// Checks the config variable AdminLoginOnlyAllowed (bool) and will only allow Admin role to login.
        /// Checks the config variable MaxBadLogins (int) to see how many bad logins are allowed.
        /// Config variables are in web.config under configSections -> appSettings.
        /// App throws an NullReferenceException if these values are not set.
        /// 
        /// </summary>
        /// <param name="model">Login model from the submitted form</param>
        /// <returns>Redirects to dashboard on success. Re-display form on failure.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        { 
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                //Get configured values and get the users this template is for an intranet application.
                //Note AdminLoginOnlyallowed is commented out as 
                //bool AdminLoginOnlyAllowed = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["AdminLoginOnlyAllowed"]);
                int maxBadLogins = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxBadLogins"]);
                Account acc = AccountRepos.Get(model.UserName);
                
                //Trap errors
                if(acc == null)
                    ModelState.AddModelError("", "Your username or password is not correct.");
                
                if (!(bool)acc.IsEnabled)
                    ModelState.AddModelError("", "Your account is not enabled. Please contact your site administrator.");
                
                //if(AdminLoginOnlyAllowed && !User.IsInRole("Admin"))
                    //ModelState.AddModelError("", "This website is being maintained. Normal service will resume shortly.");
                
                //check details submitted
                if (ModelState.IsValid)
                {
                    if (WebSecurity.IsConfirmed(model.UserName))
                    {
                        if (WebSecurity.Login(acc.UserName, model.Password, persistCookie: model.RememberMe))
                        {
                            //use the Enable function to reset the numBad Logins to 0;
                            AccountRepos.Enable(acc.UserName);  
                            return RedirectToAction("Dashboard", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Your username or password is not correct");
                            AccountRepos.AddBadLogin(model.UserName);
                            RedirectToAction("SendNotifyFailedLoginEmail", "Email", new { username = model.UserName });
                            if (maxBadLogins > 0 && AccountRepos.GetNumBadLogins(acc.AccountId) > maxBadLogins)
                            {
                                AccountRepos.Disable(acc.UserName);
                            }
                        }                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account is not activated. Please Check Your email and activate your account.");                            
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Logs the user off the application session.
        /// </summary>
        /// <returns>returns the user to the site home page</returns>
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region registration methods
        /// <summary>
        /// Displays a the registration Form.
        /// </summary>
        /// <returns>Returns the registration from view</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            bool RegistrationAllowed = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["RegistrationAllowed"]);
            if (!RegistrationAllowed)
            {
                ViewBag.ErrorMessage = "Registration is not allowed at this time";
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
try
                {
                    string ConfirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, 
                        new { 
                            NumBadLogins = 0,
                            FirstName = model.FirstName, 
                            LastName = model.LastName, 
                            Email = model.Email, 
                            DateOfBirth = model.DateOfBirth,
                            AccessLevel=1,
                            IsLDAPAccount=false,
                            IsEnabled = false }, true);
                    
                    return RedirectToAction("SendAccountConfirmationEmail", "Email",new{username = model.UserName, token = ConfirmationToken});
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(MembershipCreateUserException))
                        ModelState.AddModelError("", ErrorCodeToString(((MembershipCreateUserException)e).StatusCode));
                    ModelState.AddModelError("", "We could not register your details, maybe the email address has already been used? if so just reset your password");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegistrationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UnAuthorisedAccessError()
        {
            ViewBag.ErrorMessage = "An access error has occurred.";
            return View("Error");
        }
        #endregion

        #region Account Confirmation Methods
        [AllowAnonymous]
        public ActionResult ConfirmAccount()
        {
            //get values from request, verify and activate account.
            string username = Request.QueryString["u"];
            string token = Request.QueryString["t"];
            bool success = WebSecurity.ConfirmAccount(username, token);
            if (success)
            {   
                AccountRepos.Enable(username);
                return RedirectToAction("ConfirmationSuccess");
            }
            ViewBag.ErrorMessage("We could not confirm your account. The confirmation token is nonly valid for 24Hours. please click here to request another confirmation email.");
            return View("Error");
        }

        [AllowAnonymous]
        public ActionResult ConfirmationSuccess()
        {
            WebSecurity.Logout();
            return View();
        }

        [AllowAnonymous]
        public ActionResult SendConfirmationToken(string username)
        {
            Account acc = AccountRepos.Get(username);
            try
            {
                string ConfirmationToken = AccountRepos.GetConfirmationToken(username);
                RedirectToAction("SendAccountConfirmationEmail", "Email", new { account = acc,token = ConfirmationToken });
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage("Could not send a confirmation token.");
                return RedirectToAction("Error");
            }
        }
        #endregion

        #region Password Reset Methods
        [AllowAnonymous]
        public ActionResult RequestChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RequestChangePassword(RequestChangePasswordModel model)
        {
            Account acc = AccountRepos.GetByEmail(model.Email);
            string token = WebSecurity.GeneratePasswordResetToken(acc.UserName, 1440);
            return RedirectToAction("SendPasswordResetEmail", "Email", new { username = acc.UserName, token = token });
        }

        [AllowAnonymous]
        public ActionResult RequestChangePasswordSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            ChangePasswordModel cpm = new ChangePasswordModel()
            {
                UserName = Request.QueryString["u"],
                Token = Request.QueryString["t"]
            };
            return View(cpm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool security = WebSecurity.ResetPassword(model.Token, model.NewPassword);
                if (security)
                    AccountRepos.Enable(model.UserName);
                    return RedirectToAction("ChangePasswordSuccess");                
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ChangePasswordSuccess()
        {
            WebSecurity.Logout();
            return View();
        }
        #endregion

        #region helper methods
        public JsonResult UserAutoComplete(string term)
        {
            List<Account> users = AccountRepos.Search(term);
            JsonResult res = Json(users.Select(x => new
            {
                id = x.AccountId,
                value = x.FirstName + " " + x.LastName,
                label = x.FirstName + " " + x.LastName
            }),JsonRequestBehavior.AllowGet);
            
            return res;
        }

        public string GetDisplayNameFromId(int id)
        {
            return AccountRepos.GetDisplayName(id);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        public ActionResult Dashboard()
        {
            return View();
        }
    
    }
}
