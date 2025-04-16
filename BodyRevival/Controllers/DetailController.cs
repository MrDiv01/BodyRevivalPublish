using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Controllers
{
    public class DetailController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DetailController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index(int id)
        {
            Packet packet  =await _dbContext.Packet.Include(x=>x.Teacher).ThenInclude(c=>c.User).FirstOrDefaultAsync(_=>_.Id == id);
            if(packet == null)
            {
                return RedirectToAction("Index", "ErrorPage");
            }
            return View(packet);
        }
    }
}
