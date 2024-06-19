using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class AboutManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AboutManageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            About about = await _dbContext.About.FirstOrDefaultAsync();
            return View(about);
        }
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            About about = await _dbContext.About.FirstOrDefaultAsync();
            return View(about);
        }
        [HttpPost]
        public async Task<IActionResult> Update(About about)
        {
            About about1 = await _dbContext.About.FirstOrDefaultAsync();
            about1.Title = about.Title;
            about1.Description = about.Description;
            about1.VideoUrl = about.VideoUrl;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddAbout()
        {
            About about = new()
            {
                Description = "ss",
                Title = "ss",
                VideoUrl = "wsw"
            };
            await _dbContext.About.AddAsync(about);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
