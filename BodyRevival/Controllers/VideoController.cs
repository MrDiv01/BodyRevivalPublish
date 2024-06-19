using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Controllers
{
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public VideoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Videos> videos =await _dbContext.Videos.ToListAsync();
            return View(videos);
        }
    }
}
