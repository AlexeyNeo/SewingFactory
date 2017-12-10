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
    public class RawMaterialPController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /RawMaterialP/
        public ActionResult Index(int Id)
        {
            var Model = db.Model.Find(Id);
            ViewBag.Model = Model.Name;
            ViewBag.Id = Model.Id;
            var rawmaterialp = db.RawMaterialP.Include(r => r.RawMaterials).Where(r => r.Model1.Id==Model.Id);
            return View(rawmaterialp.ToList());
        }

        // GET: /RawMaterialP/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialP rawmaterialp = db.RawMaterialP.Find(id);
            if (rawmaterialp == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialp);
        }

        // GET: /RawMaterialP/Create
        public ActionResult Create(int id)
        {
            var model = db.Model.Find(id);
            ViewBag.Model = model.Id;
            ViewBag.Name = model.Name;
            ViewBag.RawMaterial = new SelectList(db.RawMaterials, "Id", "Name");
            return View();
        }

        // POST: /RawMaterialP/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RawMaterial,Model,Quantity")] RawMaterialP rawmaterialp)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterialP.Add(rawmaterialp);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = rawmaterialp.Model });
            }

            ViewBag.Model = new SelectList(db.Model, "Id", "Name", rawmaterialp.Model);
            ViewBag.RawMaterial = new SelectList(db.RawMaterials, "Id", "Name", rawmaterialp.RawMaterial);
            return View(rawmaterialp);
        }

        // GET: /RawMaterialP/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialP rawmaterialp = db.RawMaterialP.Find(id);
            if (rawmaterialp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Model = new SelectList(db.Model, "Id", "Name", rawmaterialp.Model);
            ViewBag.RawMaterial = new SelectList(db.RawMaterials, "Id", "Name", rawmaterialp.RawMaterial);
            return View(rawmaterialp);
        }

        // POST: /RawMaterialP/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RawMaterial,Model,Quantity")] RawMaterialP rawmaterialp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialp).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", new { id =rawmaterialp.Model });
            }
            ViewBag.Model = new SelectList(db.Model, "Id", "Name", rawmaterialp.Model);
            ViewBag.RawMaterial = new SelectList(db.RawMaterials, "Id", "Name", rawmaterialp.RawMaterial);
            return View(rawmaterialp);
        }

        // GET: /RawMaterialP/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialP rawmaterialp = db.RawMaterialP.Find(id);
            if (rawmaterialp == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialp);
        }

        // POST: /RawMaterialP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            RawMaterialP rawmaterialp = db.RawMaterialP.Find(id);
             ViewBag.Id = rawmaterialp.Model;
            db.RawMaterialP.Remove(rawmaterialp);
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = ViewBag.Id });
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
