using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class SalaryHistoryController : Controller

    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /SalaryHistory/
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.WorkingGroup1).Where(o => o.Done == o.Quantity);
           return View(order.ToList());
        }
        public ActionResult Order(short id)
        {
           var order= db.Order.Find(id);
           if (order == null)
               return HttpNotFound();
           else
           {
               var salary = db.SalaryHistory.Where(s => s.PostOrder1.OrderId==id);
               var Ord=db.Order.Find(id);

               ViewBag.Order = Ord.Name;
               return View(salary.ToList());
           }
         
        }
        // GET: /SalaryHistory/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryHistory salaryhistory = db.SalaryHistory.Find(id);
            if (salaryhistory == null)
            {
                return HttpNotFound();
            }
            return View(salaryhistory);
        }

        // GET: /SalaryHistory/Create
        public ActionResult Create()
        {
            ViewBag.Emploees = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.PostOrder = new SelectList(db.PostOrder, "Id", "Id");
            return View();
        }

        // POST: /SalaryHistory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Emploees,Sum,Date,PostOrder,Payment")] SalaryHistory salaryhistory)
        {
            if (ModelState.IsValid)
            {
                db.SalaryHistory.Add(salaryhistory);
                db.SaveChanges();
                return RedirectToAction("Order");
            }

            ViewBag.Emploees = new SelectList(db.Employees, "Id", "FullName", salaryhistory.Emploees);
            ViewBag.PostOrder = new SelectList(db.PostOrder, "Id", "Id", salaryhistory.PostOrder);
            return View(salaryhistory);
        }

        // GET: /SalaryHistory/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryHistory salaryhistory = db.SalaryHistory.Find(id);
            if (salaryhistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Emploees = new SelectList(db.Employees, "Id", "FullName", salaryhistory.Emploees);
            ViewBag.PostOrder = new SelectList(db.PostOrder, "Id", "Id", salaryhistory.PostOrder);
            return View(salaryhistory);
        }

        // POST: /SalaryHistory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Emploees,Sum,Date,PostOrder,Payment")] SalaryHistory salaryhistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryhistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Emploees = new SelectList(db.Employees, "Id", "FullName", salaryhistory.Emploees);
            ViewBag.PostOrder = new SelectList(db.PostOrder, "Id", "Id", salaryhistory.PostOrder);
            return View(salaryhistory);
        }

        // GET: /SalaryHistory/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryHistory salaryhistory = db.SalaryHistory.Find(id);
            if (salaryhistory == null)
            {
                return HttpNotFound();
            }
            return View(salaryhistory);
        }

        // POST: /SalaryHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            SalaryHistory salaryhistory = db.SalaryHistory.Find(id);
            db.SalaryHistory.Remove(salaryhistory);
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
