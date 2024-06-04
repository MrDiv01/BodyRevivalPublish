using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
