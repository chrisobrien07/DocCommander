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
    public class SysListItemController : Controller
    {
        private DocCommanderEntities db = new DocCommanderEntities();

        
        //
        // GET: /SysListItem/

        public ActionResult Index(int sysListId)
        {
            var syslistitems = db.SysListItems.Where(x=>x.SysListId==sysListId);
            ViewBag.SysListId = sysListId;
            return PartialView(syslistitems.ToList());
        }

        //
        // GET: /SysListItem/Details/5

        public ActionResult Details(int id = 0)
        {
            SysListItem syslistitem = db.SysListItems.Find(id);
            if (syslistitem == null)
            {
                return HttpNotFound();
            }
            return PartialView(syslistitem);
        }

        //
        // GET: /SysListItem/Create

        public ActionResult Create(int SysListId)
        {
            ViewBag.SysListId = SysListId;
            return PartialView();
        }

        //
        // POST: /SysListItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SysListItem syslistitem)
        {
            if (ModelState.IsValid)
            {
                db.SysListItems.Add(syslistitem);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage=ex.Message;
                    return View("Error");
                }
                
                return RedirectToAction("Index", new { sysListId = syslistitem.SysListId });
            }
            ViewBag.SysListId = syslistitem.SysListId;
            return PartialView(syslistitem);
        }

        //
        // GET: /SysListItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SysListItem syslistitem = db.SysListItems.Find(id);
            if (syslistitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.SysListId = syslistitem.SysListId;
            return PartialView(syslistitem);
        }

        //
        // POST: /SysListItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysListItem syslistitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syslistitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { sysListId = syslistitem.SysListId });
            }
            ViewBag.SysListId = syslistitem.SysListId;
            return PartialView(syslistitem);
        }

        //
        // GET: /SysListItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SysListItem syslistitem = db.SysListItems.Find(id);
            if (syslistitem == null)
            {
                return HttpNotFound();
            }
            return PartialView(syslistitem);
        }

        //
        // POST: /SysListItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysListItem syslistitem = db.SysListItems.Find(id);
            db.SysListItems.Remove(syslistitem);
            db.SaveChanges();
            return RedirectToAction("Index", new {sysListId=syslistitem.SysListId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}