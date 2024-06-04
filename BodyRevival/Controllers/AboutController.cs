using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
