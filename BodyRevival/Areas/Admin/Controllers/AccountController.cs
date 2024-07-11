using BodyRevival.Areas.Admin.ViewModels;
using BodyRevival.Data;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(adminLogin.Mail);
            if (user == null)
            {
                ModelState.AddModelError("", "Mail or Password is Incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, adminLogin.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is Incorrect");
                return View();
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Teacher"))
            {
                return RedirectToAction("Profile", "ConfigureTeacher");
            }
            else if (role.Contains("SuperAdmin"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return Forbid();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {

                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("LogIn");
        }
        [HttpGet]

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Register()
        {
            ViewBag.packets = await _dbContext.Packet.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(model.UserName);
            if (member != null)
            {
                ModelState.AddModelError("Username", "USerName Has Taken");
                return View();
            }
            AppUser memberMail = await _userManager.FindByEmailAsync(model.Email);
            if (memberMail != null)
            {
                ModelState.AddModelError("Email", "USerName Has Taken");
                return View();
            }
            member = new AppUser
            {
                FullName = model.Name,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(member, model.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            var roleresult = await _userManager.AddToRoleAsync(member, "student");
            if (!roleresult.Succeeded)
            {
                foreach (var err in roleresult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            var packet = _dbContext.Packet.FirstOrDefaultAsync(x=>x.Id == model.PacketId);
            Student student = new()
            {
                UserId = member.Id,
                PacketId = model.PacketId,
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> TeacherRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeacherRegister(TeacherRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByEmailAsync(model.Email);
            if (member != null)
            {
                ModelState.AddModelError("", "Email Has Taken");
                return View();
            }
            AppUser memberUserName = await _userManager.FindByNameAsync(model.UserName);
            if (memberUserName != null)
            {
                ModelState.AddModelError("", "Username Has Taken");
                return View();
            }
            member = new AppUser
            {
                FullName = model.Name,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(member, model.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            var roleresult = await _userManager.AddToRoleAsync(member, "teacher");
            if (!roleresult.Succeeded)
            {
                foreach (var err in roleresult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            Teacher teacher = new()
            {
                Weight = 0,
                Height = 0,
                Description = "test",
                UserId = member.Id,
                Image = "test",
                IsUpdated = false
            };
            await _dbContext.Teacher.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
