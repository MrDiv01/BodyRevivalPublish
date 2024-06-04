using BodyRevival.Areas.Admin.ViewModels;
using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            AppUser user = await _userManager.FindByNameAsync(adminLogin.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "UserName or Password is Incorrect");
                return View();
            }
            var result =await _signInManager.PasswordSignInAsync(user,adminLogin.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is Incorrect");
                return View();
            }
            return RedirectToAction("Index","Dashboard");
        }

        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {

            await _signInManager.SignOutAsync();
            }
            return RedirectToAction("LogIn");
        }

        [Authorize(Roles ="SuperAdmin")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member =await _userManager.FindByNameAsync(model.UserName);
            if(member != null)
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
                FullName = model.UserName,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(member,model.Password);
            if (!result.Succeeded)
            {
                foreach(var err in result.Errors)
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
            return RedirectToAction("Index","Dashboard");
        }
    }
}
