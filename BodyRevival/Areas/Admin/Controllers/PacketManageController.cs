using BodyRevival.Areas.Admin.ViewModels;
using BodyRevival.Data;
using BodyRevival.Helper;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PacketManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public PacketManageController(ApplicationDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Packet> packets =await _dbContext.Packet.Include(x => x.Teacher).ThenInclude(c => c.User).ToListAsync();
            return View(packets);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.teachers = await _dbContext.Teacher.Include(_ => _.User).ToListAsync(); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Packet packet)
        {
            if (packet.ImageFile.ContentType != "image/png" && packet.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = packet.ImageFile.FileName;

            packet.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads", packet.ImageFile);

            await _dbContext.Packet.AddAsync(packet);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
