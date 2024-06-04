using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
