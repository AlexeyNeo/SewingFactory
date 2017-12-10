using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class EmploeesOrderController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /EmploeesOrder/
        public ActionResult Index()
        {
            var order = db.Order;
            return View(order.ToList());
        }

        // GET: /EmploeesOrder/Details/5

        public ActionResult EmploeesOrder(short? id)
        {

         
            var post = db.EmploeesOrder.Where(p => p.OrderId == id);

            return View(post);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploeesOrder emploeesorder = db.EmploeesOrder.Find(id);
            if (emploeesorder == null)
            {
                return HttpNotFound();
            }
            return View(emploeesorder);
        }

        // GET: /EmploeesOrder/Create
        public ActionResult Create()
        {
            ViewBag.EmploeesId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name");
            return View();
        }

        // POST: /EmploeesOrder/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EmploeesId,OrderId,Done")] EmploeesOrder emploeesorder)
        {
            if (ModelState.IsValid)
            {
                db.EmploeesOrder.Add(emploeesorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmploeesId = new SelectList(db.Employees, "Id", "FullName", emploeesorder.EmploeesId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", emploeesorder.OrderId);
            return View(emploeesorder);
        }

        // GET: /EmploeesOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploeesOrder emploeesorder = db.EmploeesOrder.Find(id);
            if (emploeesorder == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmploeesId = new SelectList(db.Employees, "Id", "FullName", emploeesorder.EmploeesId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", emploeesorder.OrderId);
            return View(emploeesorder);
        }

        // POST: /EmploeesOrder/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EmploeesId,OrderId,Done")] EmploeesOrder emploeesorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploeesorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmploeesOrder", new {id=emploeesorder.OrderId});
            }
            ViewBag.EmploeesId = new SelectList(db.Employees, "Id", "FullName", emploeesorder.EmploeesId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", emploeesorder.OrderId);
            return View(emploeesorder);
        }

        // GET: /EmploeesOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploeesOrder emploeesorder = db.EmploeesOrder.Find(id);
            if (emploeesorder == null)
            {
                return HttpNotFound();
            }
            return View(emploeesorder);
        }

        // POST: /EmploeesOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmploeesOrder emploeesorder = db.EmploeesOrder.Find(id);
            db.EmploeesOrder.Remove(emploeesorder);
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
