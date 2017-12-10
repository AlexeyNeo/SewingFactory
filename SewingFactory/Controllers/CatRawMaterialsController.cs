using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class CatRawMaterialsController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /CatRawMaterials/
        public ActionResult Index()
        {
            return View(db.CatRawMaterials.ToList());
        }

        // GET: /CatRawMaterials/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatRawMaterials catrawmaterials = db.CatRawMaterials.Find(id);
            if (catrawmaterials == null)
            {
                return HttpNotFound();
            }
            return View(catrawmaterials);
        }

        // GET: /CatRawMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CatRawMaterials/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] CatRawMaterials catrawmaterials)
        {
            if (ModelState.IsValid)
            {
                db.CatRawMaterials.Add(catrawmaterials);
                db.SaveChanges();
                return RedirectToAction("Index", "RawMaterials");
            }

            return View(catrawmaterials);
        }

        // GET: /CatRawMaterials/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatRawMaterials catrawmaterials = db.CatRawMaterials.Find(id);
            if (catrawmaterials == null)
            {
                return HttpNotFound();
            }
            return View(catrawmaterials);
        }

        // POST: /CatRawMaterials/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] CatRawMaterials catrawmaterials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catrawmaterials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catrawmaterials);
        }

        // GET: /CatRawMaterials/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatRawMaterials catrawmaterials = db.CatRawMaterials.Find(id);
            if (catrawmaterials == null)
            {
                return HttpNotFound();
            }
            return View(catrawmaterials);
        }

        // POST: /CatRawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            CatRawMaterials catrawmaterials = db.CatRawMaterials.Find(id);
            db.CatRawMaterials.Remove(catrawmaterials);
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
