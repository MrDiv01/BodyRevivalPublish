using BodyRevival.Data;
using BodyRevival.Models;
using BodyRevival.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AboutController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            AboutViewModel viewModel = new()
            {
                teachers = await _dbContext.Teacher.Include(x => x.User).Where(_ => _.IsUpdated == true).ToListAsync(),
                about = await _dbContext.About.FirstOrDefaultAsync()
            };
            if(viewModel.about == null)
                return RedirectToAction("Index", "ErrorPage");
            return View(viewModel);
        }
    }
}
