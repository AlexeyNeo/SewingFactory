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
    public class EmployeesController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /Employees/
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.PostEmploees).Include(e => e.WorkingGroup1).ToList();
            return View(employees);
        }

        // GET: /Employees/Details/5
        public ActionResult report(int Id)
        {
            ViewBag.Employees = db.Employees.Find(Id);
            var or = db.PaymentHistory.Where(p => p.SalaryHistory.Employees.Id == Id);
            return View(or.ToList());

        }
        [HttpPost]
        public ActionResult report(int Id, DateTime data, DateTime data1)
        {
            ViewBag.Employees=db.Employees.Find(Id);
                var emp = db.PaymentHistory.Include(p => p.SalaryHistory.Employees).Where(p => p.SalaryHistory.Emploees == Id && p.Date >= data && p.Date <= data1);
                return View(emp.ToList());
            
        }
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: /Employees/Create
        public ActionResult Create()
        {
            ViewBag.Post = new SelectList(db.PostEmploees, "Id", "Name");
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name");
            return View();
        }

        // POST: /Employees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FullName,Phone,WorkingGroup,Post")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Post = new SelectList(db.PostEmploees, "Id", "Name", employees.Post);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", employees.WorkingGroup);
            return View(employees);
        }

        // GET: /Employees/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.Post = new SelectList(db.PostEmploees, "Id", "Name", employees.Post);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", employees.WorkingGroup);
            return View(employees);
        }

        // POST: /Employees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FullName,Phone,WorkingGroup,Post")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Post = new SelectList(db.PostEmploees, "Id", "Name", employees.Post);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", employees.WorkingGroup);
            return View(employees);
        }

        // GET: /Employees/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: /Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
