using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _dbContext.Teacher.Include(x => x.User).Where(_ => _.IsUpdated == true).ToListAsync();
           if(teachers.Count == 0)
                return NotFound();  
            return View(teachers);
        }
    }
}
