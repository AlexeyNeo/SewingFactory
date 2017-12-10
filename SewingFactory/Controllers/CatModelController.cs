using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class CatModelController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /CatModel/
        public ActionResult Index()
        {
            return View(db.CatModel.ToList());
        }

        // GET: /CatModel/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatModel catmodel = db.CatModel.Find(id);
            if (catmodel == null)
            {
                return HttpNotFound();
            }
            return View(catmodel);
        }

        // GET: /CatModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CatModel/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] CatModel catmodel)
        {
            if (ModelState.IsValid)
            {
                db.CatModel.Add(catmodel);
                db.SaveChanges();
                return RedirectToAction("Index", "Model");
            }

            return View(catmodel);
        }

        // GET: /CatModel/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatModel catmodel = db.CatModel.Find(id);
            if (catmodel == null)
            {
                return HttpNotFound();
            }
            return View(catmodel);
        }

        // POST: /CatModel/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] CatModel catmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catmodel);
        }

        // GET: /CatModel/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatModel catmodel = db.CatModel.Find(id);
            if (catmodel == null)
            {
                return HttpNotFound();
            }
            return View(catmodel);
        }

        // POST: /CatModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            CatModel catmodel = db.CatModel.Find(id);
            db.CatModel.Remove(catmodel);
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
