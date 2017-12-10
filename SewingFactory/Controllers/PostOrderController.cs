using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class PostOrderController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /PostOrder/
        public ActionResult PostOrder(short? id)
        {
            
           
                var post = db.PostOrder.Where(p => p.OrderId==id).OrderByDescending(p => p.Id);
            
            return View(post);
        }
        public ActionResult Index()
        {
            var order = db.Order;
            
            return View(order);
        }

        // GET: /PostOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOrder postorder = db.PostOrder.Find(id);
            if (postorder == null)
            {
                return HttpNotFound();
            }
            return View(postorder);
        }

        // GET: /PostOrder/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name");
            ViewBag.PostEmploees = new SelectList(db.PostEmploees, "Id", "Name");
            return View();
        }

        // POST: /PostOrder/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,OrderId,PostEmploees,Payment")] PostOrder postorder)
        {
            if (ModelState.IsValid)
            {
                db.PostOrder.Add(postorder);
                db.SaveChanges();
                return RedirectToAction("PostOrder");
            }

            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", postorder.OrderId);
            ViewBag.PostEmploees = new SelectList(db.PostEmploees, "Id", "Name", postorder.PostEmploees);
            return View(postorder);
        }

        // GET: /PostOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOrder postorder = db.PostOrder.Find(id);
            if (postorder == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", postorder.OrderId);
            ViewBag.PostEmploees = new SelectList(db.PostEmploees, "Id", "Name", postorder.PostEmploees);
            return View(postorder);
        }

        // POST: /PostOrder/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,OrderId,PostEmploees,Payment")] PostOrder postorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PostOrder", new {id= postorder.OrderId });
            }
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Name", postorder.OrderId);
            ViewBag.PostEmploees = new SelectList(db.PostEmploees, "Id", "Name", postorder.PostEmploees);
            return View(postorder);
        }

        // GET: /PostOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostOrder postorder = db.PostOrder.Find(id);
            if (postorder == null)
            {
                return HttpNotFound();
            }
            return View(postorder);
        }

        // POST: /PostOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostOrder postorder = db.PostOrder.Find(id);
            db.PostOrder.Remove(postorder);
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
