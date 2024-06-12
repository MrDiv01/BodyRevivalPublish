using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Areas.Admin.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
