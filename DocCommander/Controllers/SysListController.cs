using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocCommander.Data;

namespace DocCommander.Controllers
{
    public class SysListController : Controller
    {
        private DocCommanderEntities db = new DocCommanderEntities();

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult GetLists()
        {
            return PartialView("_SysListDisplayAll", db.SysLists.ToList());
        }

        public ActionResult GetItems(int sysListId)
        {
            return PartialView("_SysListItemsDisplay", db.SysListItems.Where(x => x.SysListId == sysListId).OrderBy(x => x.Value));
        }
        

        


        //
        // GET: /SysList/

        public ActionResult Index()
        {
            return RedirectToAction("Manage");
        }

        //
        // GET: /SysList/Details/5

        public ActionResult Details(int id = 0)
        {
            SysList syslist = db.SysLists.Find(id);
            if (syslist == null)
            {
                return HttpNotFound();
            }
            return PartialView(syslist);
        }

        //
        // GET: /SysList/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SysList model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("GetLists");
            }

            return PartialView(model);
        }

        //
        // GET: /SysList/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SysList syslist = db.SysLists.Find(id);
            if (syslist == null)
            {
                return HttpNotFound();
            }
            return PartialView(syslist);
        }

        //
        // POST: /SysList/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysList syslist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syslist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(syslist);
        }

        //
        // GET: /SysList/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SysList syslist = db.SysLists.Find(id);
            if (syslist == null)
            {
                return HttpNotFound();
            }
            return PartialView(syslist);
        }

        //
        // POST: /SysList/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysList syslist = db.SysLists.Find(id);
            db.SysLists.Remove(syslist);
            db.SaveChanges();
            return RedirectToAction("GetLists");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}