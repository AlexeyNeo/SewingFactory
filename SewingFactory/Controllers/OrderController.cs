using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class OrderController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /Order/
        public ActionResult Index(short? id)
        {
            switch (id)
            {
              case 1: var order1 = db.Order.Include(o => o.Customers).Include(o => o.Model1).Include(o => o.WorkingGroup1).Where(o => o.Status==1);
                    ViewBag.order = "Только активные заказы";
                    return View(order1.ToList()); 
               case 0: var order2 = db.Order.Include(o => o.Customers).Include(o => o.Model1).Include(o => o.WorkingGroup1).Where(o => o.Status==0);
                    ViewBag.order = "Только неактивные заказы";
                    return View(order2.ToList());
               default: var order = db.Order.Include(o => o.Customers).Include(o => o.Model1).Include(o => o.WorkingGroup1);
                    ViewBag.order = "Все заказы";
                    return View(order.ToList()); 
            }
        }

  
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Order/Create
        public ActionResult Create()
        {
            ViewBag.Costomer = new SelectList(db.Customers, "Id", "FullName");
            ViewBag.Model = new SelectList(db.Model, "Id", "Name");
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name");
            return View();
        }

        // POST: /Order/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Costomer,Name,WorkingGroup,Date,Quantity,DateOfCompletion,Model,Size,Cost,sum,Done,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Costomer = new SelectList(db.Customers, "Id", "FullName", order.Costomer);
            ViewBag.Model = new SelectList(db.Model, "Id", "Name", order.Model);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", order.WorkingGroup);
            return View(order);
        }
         
        
         
         public ActionResult report(int? LeftDate, int? rightDate)
         {
             if (LeftDate != null && rightDate != null)
             {
                 var datet = db.Order.Find(LeftDate);
                 var datett = db.Order.Find(rightDate);
                 var order = db.Order.Where(o => o.DateOfCompletion >= datet.DateOfCompletion && o.DateOfCompletion <= datett.DateOfCompletion && o.Status == 0);
                 return View(order.ToList());
             }
             else
             {
                 var or = db.Order.Where(o => o.Status == 0);
                 return View(or.ToList());
             }
         }
        [HttpPost]
         public ActionResult report(DateTime? LeftDate, DateTime? rightDate)
        {
            if (LeftDate == null && rightDate == null) return HttpNotFound();

            if(LeftDate == null) LeftDate = DateTime.Now;
            if (rightDate == null) rightDate = DateTime.Now;

            var order = db.Order.Where(o => o.DateOfCompletion >= LeftDate && o.DateOfCompletion <= rightDate && o.Status == 0);
             return View(order.ToList());
             
         }

        // GET: /Order/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
        
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Costomer = new SelectList(db.Customers, "Id", "FullName", order.Costomer);
            ViewBag.Model = new SelectList(db.Model, "Id", "Name", order.Model);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", order.WorkingGroup);
           
            return View(order);
        }

        // POST: /Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Costomer,Name,WorkingGroup,Date,Quantity,DateOfCompletion,Model,Size,Cost,sum,Done,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Quantity < order.Done)
                {
                    ViewBag.Err = "Вы не можете обработать больше, чем было заказано.";
                    return View("error");
                }
                else { 
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                     }
            }
            ViewBag.Costomer = new SelectList(db.Customers, "Id", "FullName", order.Costomer);
            ViewBag.Model = new SelectList(db.Model, "Id", "Name", order.Model);
            ViewBag.WorkingGroup = new SelectList(db.WorkingGroup, "Id", "Name", order.WorkingGroup);
            //ViewBag.ListEmployees = new SelectList(db.);
            return View(order);
        }

        // GET: /Order/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
