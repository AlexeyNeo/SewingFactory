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
    public class PaymentHistoryController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /PaymentHistory/
        public ActionResult Index(int id)
        {
             ViewBag.Sum=0;
            var paymenthistory = db.PaymentHistory.Include(p => p.SalaryHistory).Where(p => p.SalaryId==id);
              
             
            
                
            return View(paymenthistory.ToList());
        }

        // GET: /PaymentHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentHistory paymenthistory = db.PaymentHistory.Find(id);
            if (paymenthistory == null)
            {
                return HttpNotFound();
            }
            return View(paymenthistory);
        }

        // GET: /PaymentHistory/Create
        public ActionResult Create()
        {
            ViewBag.SalaryId = new SelectList(db.SalaryHistory, "Id", "Id");
            return View();
        }

        // POST: /PaymentHistory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,SalaryId,Date,Sum")] PaymentHistory paymenthistory)
        {
            if (ModelState.IsValid)
            {
                db.PaymentHistory.Add(paymenthistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalaryId = new SelectList(db.SalaryHistory, "Id", "Id", paymenthistory.SalaryId);
            return View(paymenthistory);
        }

        // GET: /PaymentHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentHistory paymenthistory = db.PaymentHistory.Find(id);
            if (paymenthistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaryId = new SelectList(db.SalaryHistory, "Id", "Id", paymenthistory.SalaryId);
            return View(paymenthistory);
        }

        // POST: /PaymentHistory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,SalaryId,Date,Sum")] PaymentHistory paymenthistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymenthistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalaryId = new SelectList(db.SalaryHistory, "Id", "Id", paymenthistory.SalaryId);
            return View(paymenthistory);
        }

        // GET: /PaymentHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentHistory paymenthistory = db.PaymentHistory.Find(id);
            if (paymenthistory == null)
            {
                return HttpNotFound();
            }
            return View(paymenthistory);
        }

        // POST: /PaymentHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentHistory paymenthistory = db.PaymentHistory.Find(id);
            db.PaymentHistory.Remove(paymenthistory);
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
