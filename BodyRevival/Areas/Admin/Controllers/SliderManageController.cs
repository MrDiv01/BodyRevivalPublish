using BodyRevival.Data;
using BodyRevival.Helper;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SliderManageController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        public SliderManageController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<HomeSlider> slider = await _context.HomeSliders.ToListAsync();
            if (slider == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            return View(slider);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeSlider homedec)
        {
            if (homedec.ImageFile.ContentType != "image/png" && homedec.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = homedec.ImageFile.FileName;

            homedec.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads", homedec.ImageFile);

            HomeSlider slider = new HomeSlider()
            {
                ButtonText = homedec.ButtonText,
                ButtonUrl = homedec.ButtonUrl,
                FirstMotivationWord = homedec.FirstMotivationWord,
                SecondMotivationWord = homedec.SecondMotivationWord,
                Image = homedec.Image,
            };
            await _context.HomeSliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TempData["Id"] = id;
            HomeSlider homedec = await _context.HomeSliders.FindAsync(id);
            if (homedec == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            if (homedec == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            

            return View(homedec);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HomeSlider homedec)
        {
            HomeSlider exsTitl = await _context.HomeSliders.FirstOrDefaultAsync(x => x.Id == (int)TempData["id"]);
            if (exsTitl == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");

            }
            if (homedec.ImageFile.ContentType != "image/png" && homedec.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = homedec.ImageFile.FileName;
            FileMeneger.DeleteFile(_env.WebRootPath, "uploads", exsTitl.Image);
            exsTitl.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads", homedec.ImageFile);
            exsTitl.FirstMotivationWord = homedec.FirstMotivationWord;
            exsTitl.SecondMotivationWord = homedec.SecondMotivationWord;
            exsTitl.ButtonText = homedec.ButtonText;
            exsTitl.ButtonUrl = homedec.ButtonUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HomeSlider slider = await _context.HomeSliders.FindAsync(id);
            if (slider == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            FileMeneger.DeleteFile(_env.WebRootPath, "uploads", slider.Image);
            _context.HomeSliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}

