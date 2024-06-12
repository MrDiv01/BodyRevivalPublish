using BodyRevival.Data;
using BodyRevival.Helper;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Teacher")]

    public class ConfigureTeacherController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ConfigureTeacherController(IWebHostEnvironment env, ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            //AddClass();

            Teacher users = await _context.Teacher.Where(x => x.UserId == user.Id ).FirstOrDefaultAsync();
            return View(users);
        }
        [HttpGet]
        public IActionResult TeacherUpdate(string Id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeacherUpdate(Teacher teacher)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            Teacher updateTeacher = await _context.Teacher.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (teacher.ImageFile.ContentType != "image/png" && teacher.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = teacher.ImageFile.FileName;
            FileMeneger.DeleteFile(_env.WebRootPath, "uploads", updateTeacher.Image);
            updateTeacher.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads/HomePage", teacher.ImageFile);
            updateTeacher.UserId = user.Id;
            updateTeacher.IsUpdated = true;
            updateTeacher.Weight = teacher.Weight;
            updateTeacher.Height = teacher.Height;
            updateTeacher.Description = teacher.Description;
            updateTeacher.Speciality = teacher.Speciality;

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }
    }
}
