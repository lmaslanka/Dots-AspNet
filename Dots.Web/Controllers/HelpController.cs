namespace Dots.Web.Controllers
{
    using System.Web.Mvc;
    using dots.database;

    public class HelpController : BaseController
    {
        public ActionResult Index()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);

                return View();
            }
        }

        public ActionResult Updater()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);

                return View();
            }
        }

        public ActionResult Editor()
        {
            using (var db = new DotsContext())
            {
                GetUser(db);

                return View();
            }
        }
    }
}