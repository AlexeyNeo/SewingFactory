using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class RawMaterialsController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /RawMaterials/
        public ActionResult Index()
        {
            var rawmaterials = db.RawMaterials.Include(r => r.CatRawMaterials);
            return View(rawmaterials.ToList());
        }

        // GET: /RawMaterials/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawmaterials = db.RawMaterials.Find(id);
            if (rawmaterials == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterials);
        }

        // GET: /RawMaterials/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.CatRawMaterials, "Id", "Name");
            return View();
        }

        // POST: /RawMaterials/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Category,Description,Width,length,Count,Cost")] RawMaterials rawmaterials)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterials.Add(rawmaterials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.CatRawMaterials, "Id", "Name", rawmaterials.Category);
            return View(rawmaterials);
        }

        // GET: /RawMaterials/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawmaterials = db.RawMaterials.Find(id);
            if (rawmaterials == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.CatRawMaterials, "Id", "Name", rawmaterials.Category);
            return View(rawmaterials);
        }

        // POST: /RawMaterials/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Category,Description,Width,length,Count,Cost")] RawMaterials rawmaterials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.CatRawMaterials, "Id", "Name", rawmaterials.Category);
            return View(rawmaterials);
        }

        // GET: /RawMaterials/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawmaterials = db.RawMaterials.Find(id);
            if (rawmaterials == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterials);
        }

        // POST: /RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            RawMaterials rawmaterials = db.RawMaterials.Find(id);
            db.RawMaterials.Remove(rawmaterials);
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
