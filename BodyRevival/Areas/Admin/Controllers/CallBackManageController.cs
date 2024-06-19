using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CallBackManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CallBackManageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Communication> contacts = await _dbContext.Communications.ToListAsync();
            return View(contacts);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Communication communication = await _dbContext.Communications.FirstOrDefaultAsync(x => x.Id == id);
             _dbContext.Communications.Remove(communication);
            await _dbContext.SaveChangesAsync();

            List<Communication> contacts = await _dbContext.Communications.ToListAsync();
            return RedirectToAction("index");
        }
    }
}
