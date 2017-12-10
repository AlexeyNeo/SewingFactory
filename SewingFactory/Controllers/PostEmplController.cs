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
    public class PostEmplController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /PostEmpl/
        public ActionResult Index()
        {
            return View(db.PostEmploees.ToList());
        }

        // GET: /PostEmpl/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostEmploees postemploees = db.PostEmploees.Find(id);
            if (postemploees == null)
            {
                return HttpNotFound();
            }
            return View(postemploees);
        }

        // GET: /PostEmpl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PostEmpl/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] PostEmploees postemploees)
        {
            if (ModelState.IsValid)
            {
                db.PostEmploees.Add(postemploees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postemploees);
        }

        // GET: /PostEmpl/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostEmploees postemploees = db.PostEmploees.Find(id);
            if (postemploees == null)
            {
                return HttpNotFound();
            }
            return View(postemploees);
        }

        // POST: /PostEmpl/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] PostEmploees postemploees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postemploees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postemploees);
        }

        // GET: /PostEmpl/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostEmploees postemploees = db.PostEmploees.Find(id);
            if (postemploees == null)
            {
                return HttpNotFound();
            }
            return View(postemploees);
        }

        // POST: /PostEmpl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            PostEmploees postemploees = db.PostEmploees.Find(id);
            db.PostEmploees.Remove(postemploees);
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
