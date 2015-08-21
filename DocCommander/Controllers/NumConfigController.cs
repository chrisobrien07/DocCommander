using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocCommander.Data;
using System.Data.Entity;

namespace DocCommander.Controllers
{
    public class NumConfigController : Controller
    {
        private DocCommanderEntities db = new DocCommanderEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            return PartialView("List",db.NumConfigs.ToList());
        }

        public ActionResult Create()
        {
            var res = db.SysLists.Where(x => x.IsActive == true).Select(x=>x.Name).ToList();
            res.Insert(0,"EMPTY");
            ViewBag.ListNames = new SelectList(res);
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NumConfig model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("GetList");
            }
            var res = db.SysLists.Where(x => x.IsActive == true).Select(x => x.Name).ToList();
            res.Insert(0, "EMPTY");
            ViewBag.ListNames = new SelectList(db.SysLists.Where(x => x.IsActive == true).Select(x => x.Name).ToList());
            return PartialView(model);
        }

        public ActionResult Edit(int id)
        {
            NumConfig item = db.NumConfigs.Find(id);
            var res = db.SysLists.Where(x => x.IsActive == true).Select(x => x.Name).ToList();
            res.Insert(0, "EMPTY");
            ViewBag.ListNames = new SelectList(res);
            return PartialView(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NumConfig model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetList");
            }
            var res = db.SysLists.Where(x => x.IsActive == true).Select(x => x.Name).ToList();
            res.Insert(0, "EMPTY");
            ViewBag.ListNames = new SelectList(res);
            return PartialView(model);
        }

        public ActionResult Delete(int id = 0)
        {
            NumConfig item = db.NumConfigs.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return PartialView(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NumConfig item = db.NumConfigs.Find(id);
            db.NumConfigs.Remove(item);
            db.SaveChanges();
            return RedirectToAction("GetList");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}