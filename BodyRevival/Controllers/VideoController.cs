using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
