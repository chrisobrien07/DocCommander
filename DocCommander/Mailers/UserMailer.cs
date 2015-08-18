using Mvc.Mailer;
using DocCommander.Models;

namespace DocCommander.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
    {
        public UserMailer()
        {
            MasterName="_Layout";
        }
        
        public virtual MvcMailMessage WelcomeMessage(EmailWelcomeModel model)
        {
            ViewData.Model = model;
            return Populate(x =>
            {
                x.Subject = "Welcome to "+ model.SiteName;
                x.ViewName = "Welcome";
                x.To.Add(model.ToEmail);
            });
        }

        public MvcMailMessage ConfirmationTokenMessage(EmailConfirmationModel model)
        {
            ViewData.Model = model;
            return Populate(x =>
            {
                x.Subject = "Activate your account " + model.SiteName;
                x.ViewName = "ConfirmAccount";
                x.To.Add(model.ToEmail);
            });
        }

        public virtual MvcMailMessage NotifyFailedLoginAttemptMessage(EmailNotifyFailedLoginAttemptModel model)
        {
            ViewData.Model = model;
            return Populate(x =>
            {
                x.Subject = "Notification " + model.SiteName;
                x.ViewName = "NotifyFailedLoginAttempt";
                x.To.Add(model.ToEmail);
            });
        }

        public virtual MvcMailMessage PasswordResetMessage(EmailPasswordResetModel model)
        {
            ViewData.Model = model;
            return Populate(x =>
            {
                x.Subject = "Password Reset Request";
                x.ViewName = "PasswordResetRequest";
                x.To.Add(model.ToEmail);
            });
        }

        public virtual MvcMailMessage PasswordChangeSuccessMessage(string toEmail)
        {
            return Populate(x =>
            {
                x.Subject = "Password Change Success";
                x.ViewName = "PasswordChangeSuccess";
                x.To.Add(toEmail);
            });
        }
    }
}