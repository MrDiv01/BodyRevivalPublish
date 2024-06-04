using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
