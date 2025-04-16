using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
