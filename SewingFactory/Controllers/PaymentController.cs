using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class PaymentController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /Payment/
        public ActionResult Index()
        {
            var emploees = db.SalaryHistory.Include(e => e.PostOrder1.Order).Where(e => e.PostOrder1.Order.Status == 0).Where(e => e.Payment < e.Sum);
            
            return View(emploees.ToList());
        }

        // GET: /Payment/Details/5
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

        // GET: /Payment/Create
        public ActionResult Create(int Id)
        {
            var pay =db.SalaryHistory.Find(Id);
            ViewBag.Pay = pay.Sum - pay.Payment;
            ViewBag.SalaryId = pay.Id;
            return View();
        }

        // POST: /Payment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,SalaryId,Date,Sum")] PaymentHistory paymenthistory)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Id = paymenthistory.Id;
                var test = db.Budget.Find(1);
                var buget = db.SalaryHistory.Find(paymenthistory.Id);
                if ((buget.Sum - buget.Payment) < paymenthistory.Sum)
                {
                   
                    ViewBag.Err = "Вы пытаетесь выплатить больше, чем должны.";
                    return View("Errors");
                }
                else


                    if (test.money < paymenthistory.Sum)
                    {
                        ViewBag.Err = "В вашем буджете не достаточно средств для выдачи Зарплаты.";
                        return View("Errors");
                    }
                    else
                    {
                        
                        db.PaymentHistory.Add(paymenthistory);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
            }
            ViewBag.SalaryId = new SelectList(db.SalaryHistory, "Id", "Id", paymenthistory.SalaryId);
            return View(paymenthistory);
        }

        // GET: /Payment/Edit/5
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

        // POST: /Payment/Edit/5
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

        // GET: /Payment/Delete/5
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

        // POST: /Payment/Delete/5
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
