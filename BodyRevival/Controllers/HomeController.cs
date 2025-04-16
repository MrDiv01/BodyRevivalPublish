using BodyRevival.Data;
using BodyRevival.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BodyRevival.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel viewModel = new()
            {
                teachers = await _dbContext.Teacher.Include(x => x.User).Where(_ => _.IsUpdated == true).ToListAsync(),
                slider = await _dbContext.HomeSliders.ToListAsync(),
                packets = await _dbContext.Packet.ToListAsync(),
                lessons = await _dbContext.Lessons.ToListAsync(),
            };
            if (viewModel.teachers == null || viewModel.slider == null || viewModel.packets == null || viewModel.lessons == null)
                return RedirectToAction("Index", "ErrorPage");

            return View(viewModel);
        }
    }
}
