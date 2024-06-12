using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Areas.Admin.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePacket()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddAttendance()
        {
            return View();
        }

    }
}
