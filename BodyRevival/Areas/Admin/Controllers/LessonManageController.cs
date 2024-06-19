using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class LessonManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LessonManageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Lesson> lessons = await _dbContext.Lessons.ToListAsync();
            return View(lessons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            await _dbContext.Lessons.AddAsync(lesson);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Lesson lesson = await _dbContext.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            return View(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Lesson lesson)
        {
            Lesson lesson1 =await _dbContext.Lessons.FirstOrDefaultAsync(x=>x.Id == lesson.Id);

            lesson1.Name = lesson.Name;
            lesson1.Details = lesson.Details;
            lesson1.Duration = lesson.Duration;
            lesson1.Cost = lesson.Cost;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Lesson lesson =await _dbContext.Lessons.FirstOrDefaultAsync(x=>x.Id == id);
            _dbContext.Lessons.Remove(lesson);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
