//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocCommander.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.webpages_Roles = new HashSet<webpages_Roles>();
            this.AppEvents = new HashSet<AppEvent>();
        }
    
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public bool IsEnabled { get; set; }
        public int NumBadLogins { get; set; }
        public bool IsLDAPAccount { get; set; }
        public int AccessLevel { get; set; }
    
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
        public virtual ICollection<AppEvent> AppEvents { get; set; }
    }
}
