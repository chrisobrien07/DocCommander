using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocCommander.Models
{
    public class EmailConfirmationModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string ToEmail { get; set; }
        public string ConfirmationToken { get; set; }
        public string ConfirmationUrl { get; set; }        
    }

    public class EmailWelcomeModel
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string LoginUrl { get; set; }
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SiteHelpUrl { get; set; }
    }

    public class EmailPasswordResetModel
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string PasswordResetUrl { get; set; }
        public string PasswordResetToken { get; set; }
        public string ToEmail { get; set; }
        public string FirstName { get; set; }
        public string SiteHelpEmail { get; set; }
        public string UserName { get; set; }
    }

    public class EmailNotifyFailedLoginAttemptModel
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string ToEmail { get; set; }
        public string TimeOfAttempt { get; set; }
        public string IpAddress { get; set; }
        public string SiteHelpEmail { get; set; }
    }
}