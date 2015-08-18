
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DocCommander.Data;
using System.Data;


namespace DocCommander.Logic

{
    public class AccountRepos
    {
        public static bool GetIsLDAPAccount(string userName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = db.Accounts.SingleOrDefault(c => c.UserName == userName);
                if (acc != null)
                    return acc.IsLDAPAccount;
                return false;
            }
        }

        public static Account Get(string userName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                return db.Accounts.SingleOrDefault(c => c.UserName == userName);
            }
        }

        public static Account GetByEmail(string email)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                return db.Accounts.SingleOrDefault(c => c.Email == email);
            }
        }

        public static List<Account> Search(string term)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                return db.Accounts
                    .Where(x => x.FirstName.Contains(term) || x.LastName.Contains(term) || x.UserName.Contains(term))
                    .ToList(); 
            }
        }

        public static string GetPasswordResetToken(string userName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = db.Accounts.SingleOrDefault(c => c.UserName == userName);
                return db.webpages_Membership.SingleOrDefault(x => x.UserId == acc.AccountId).PasswordVerificationToken;
            }
        }

        public static string GetConfirmationToken(string userName)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = db.Accounts.SingleOrDefault(c => c.UserName == userName);
                return db.webpages_Membership.SingleOrDefault(x => x.UserId == acc.AccountId).ConfirmationToken;
            }
        }

        public static int GetNumBadLogins(int Id)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                return db.Accounts.Find(Id).NumBadLogins;
            }
        }

        public static void Enable(string username)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = AccountRepos.Get(username);
                acc.IsEnabled = true;
                acc.NumBadLogins = 0;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        
        public static void Disable(string username)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = AccountRepos.Get(username);
                acc.IsEnabled = false;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void AddBadLogin(string username)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc = AccountRepos.Get(username);
                acc.NumBadLogins++;
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static string GetDisplayName(int Id)
        {
            using (DocCommanderEntities db = new DocCommanderEntities())
            {
                Account acc =  db.Accounts.SingleOrDefault(x=>x.AccountId==Id);
                if (acc != null)
                {
                    return acc.FirstName + " " + acc.LastName;
                }
                return "";
            }
        }
    }
}