using Microsoft.AspNetCore.Mvc;

namespace BTZ_Transports.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
