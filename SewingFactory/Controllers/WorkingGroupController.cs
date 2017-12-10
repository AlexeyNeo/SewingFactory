using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class WorkingGroupController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /WorkingGroup/
        public ActionResult Index()
        {
            return View(db.WorkingGroup.ToList());
        }

        // GET: /WorkingGroup/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingGroup workinggroup = db.WorkingGroup.Find(id);
            if (workinggroup == null)
            {
                return HttpNotFound();
            }
            return View(workinggroup);
        }

        // GET: /WorkingGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /WorkingGroup/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] WorkingGroup workinggroup)
        {
            if (ModelState.IsValid)
            {
                db.WorkingGroup.Add(workinggroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workinggroup);
        }

        // GET: /WorkingGroup/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingGroup workinggroup = db.WorkingGroup.Find(id);
            if (workinggroup == null)
            {
                return HttpNotFound();
            }
            return View(workinggroup);
        }

        // POST: /WorkingGroup/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] WorkingGroup workinggroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workinggroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workinggroup);
        }

        // GET: /WorkingGroup/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingGroup workinggroup = db.WorkingGroup.Find(id);
            if (workinggroup == null)
            {
                return HttpNotFound();
            }
            return View(workinggroup);
        }

        // POST: /WorkingGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            WorkingGroup workinggroup = db.WorkingGroup.Find(id);
            db.WorkingGroup.Remove(workinggroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
