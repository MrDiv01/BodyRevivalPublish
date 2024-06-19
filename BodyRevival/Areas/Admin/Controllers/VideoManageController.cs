using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class VideoManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public VideoManageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Videos> videos = await _dbContext.Videos.ToListAsync();
            return View(videos);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Videos videos)
        {
            await _dbContext.Videos.AddAsync(videos);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Videos videos = await _dbContext.Videos.FirstOrDefaultAsync(x => x.Id == id);
            return View(videos);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Videos videos)
        {
            Videos about1 = await _dbContext.Videos.FirstOrDefaultAsync(x => x.Id == videos.Id);
            about1.Title = videos.Title;
            about1.Description = videos.Description;
            about1.VideoLink = videos.VideoLink;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Videos videos = await _dbContext.Videos.FirstOrDefaultAsync(x=>x.Id == id);
            _dbContext.Videos.Remove(videos);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
