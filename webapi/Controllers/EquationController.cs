using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class EquationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
