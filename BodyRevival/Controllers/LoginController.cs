using BodyRevival.Areas.Admin.ViewModels;
using BodyRevival.Data;
using BodyRevival.Models;
using BodyRevival.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email Or Password is Incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is Incorrect");
                return View();
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Teacher"))
            {
                Teacher teacher = await _dbContext.Teacher.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (teacher.IsUpdated == false)
                {

                    return RedirectToAction("Profile", "ConfigureTeacher", new { area = "Admin" });
                }
            }
                return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {

                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
