using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Compartilhado
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
