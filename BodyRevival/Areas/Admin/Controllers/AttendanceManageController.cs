using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Areas.Admin.Controllers
{
    public class AttendanceManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
