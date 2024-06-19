using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Controllers
{
    public class CallBackController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CallBackController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Communication communication)
        {
            _dbContext.Communications.Add(communication);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
