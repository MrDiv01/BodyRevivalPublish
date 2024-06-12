using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
