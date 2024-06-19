using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BodyRevival.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
           List<Blog> blogs = await _dbContext.Blogs.ToListAsync();
            if (blogs == null)
            {
                return NotFound();
            }
            return View(blogs);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            Blog blog =await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}
