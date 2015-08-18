using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocCommander.Models
{
    public class ConfigSettingsModel
    {
        private bool _AdminLoginOnlyAllowed;

        public bool AdminLoginOnlyAllowed
        {
            get 
            { 
                return _AdminLoginOnlyAllowed; 
            }
            set 
            { 
                _AdminLoginOnlyAllowed = value; 
            }
        }

        private int _MaxBadLogins;

        public int MaxBadLogins
        {
            get { return _MaxBadLogins; }
            set { _MaxBadLogins = value; }
        }


        private bool _RegistrationAllowed;

        public bool RegistrationAllowed
        {
            get { return _RegistrationAllowed; }
            set { _RegistrationAllowed = value; }
        }
        
        

        public void initialise()
        {
            _AdminLoginOnlyAllowed = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["AdminLoginOnlyAllowed"]);
            _MaxBadLogins = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxBadLogins"]);
            _RegistrationAllowed = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["RegistrationAllowed"]);
        }

        public void write()
        {
            System.Configuration.ConfigurationManager.AppSettings["AdminLoginOnlyAllowed"] = _AdminLoginOnlyAllowed.ToString();
            System.Configuration.ConfigurationManager.AppSettings["MaxBadLogins"] = _MaxBadLogins.ToString();
            System.Configuration.ConfigurationManager.AppSettings["RegistrationAllowed"] = _RegistrationAllowed.ToString();
        }

    }
}