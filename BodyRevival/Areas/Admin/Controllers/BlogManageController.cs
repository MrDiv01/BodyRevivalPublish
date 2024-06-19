using BodyRevival.Data;
using BodyRevival.Helper;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class BlogManageController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        public BlogManageController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> slider = await _context.Blogs.ToListAsync();
            if (slider == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            return View(slider);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (blog.ImageFile.ContentType != "image/png" && blog.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = blog.ImageFile.FileName;

            blog.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads", blog.ImageFile);
            blog.OrganizationTime = DateTime.Now;
            Blog blog1 = new Blog()
            {
                Title = blog.Title,
                Description = blog.Description,
                Image = blog.Image,
            };
            await _context.Blogs.AddAsync(blog1);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Blog homedec = await _context.Blogs.FindAsync(id);
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
        public async Task<IActionResult> Update(Blog blog)
        {
            Blog exsTitl = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == blog.Id);
            if (exsTitl == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");

            }
            if (blog.ImageFile.ContentType != "image/png" && blog.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            string name = blog.ImageFile.FileName;

            FileMeneger.DeleteFile(_env.WebRootPath, "uploads", exsTitl.Image);
            exsTitl.Image = FileMeneger.SaveFile(_env.WebRootPath, "uploads", blog.ImageFile);
            exsTitl.Title = blog.Title;
            exsTitl.Description = blog.Description;
            exsTitl.OrganizationTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return RedirectToAction("Index", "CustomErrorAdmin");
            }
            FileMeneger.DeleteFile(_env.WebRootPath, "uploads", blog.Image);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
