using System.Linq;
using System.Web.Mvc;
using SewingFactory.Models;

namespace SewingFactory.Controllers
{
    public class BugetController : Controller
    {
        private SewingFactoryEntities db = new SewingFactoryEntities();

        // GET: /Buget/
        public ActionResult Index()
        {
            return View(db.Budget.ToList());
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
