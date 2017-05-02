using System.Web.Mvc;

namespace Company.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return Redirect("/swagger");
            return View();
        }
    }
}
