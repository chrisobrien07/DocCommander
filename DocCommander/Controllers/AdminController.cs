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
    [Authorize(Roles="Admin")]
    public class AdminController: Controller
    {
        public ActionResult EditConfigSettings()
        {
            ConfigSettingsModel cfm = new ConfigSettingsModel();
            cfm.initialise();
            return View(cfm);
        }

        [HttpPost]
        public ActionResult EditConfigSettings(ConfigSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                model.write();
            }
            return View();
        }

        public ActionResult EditUser(string username)
        {
            Account acc = AccountRepos.Get(username);
            return View(acc);
        }

        public ActionResult AddUserToRoles(string username)
        {
            Account acc = AccountRepos.Get(username);
            return View(acc);
        }


    }
}
