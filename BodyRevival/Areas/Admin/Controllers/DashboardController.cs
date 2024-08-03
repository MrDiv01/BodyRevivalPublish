using BodyRevival.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BodyRevival.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
             _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        FullName = "Ali",
        //        UserName = "Admin",
        //        Email = "bodyrevivals@gmail.com"
        //    };
        //    var result = await _userManager.CreateAsync(user, "BodyRevivals001.");
        //    return Ok(result);
        //}
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Teacher");
        //    IdentityRole role3 = new IdentityRole("Student");
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok("Created");
        //}
        //public async Task<IActionResult> AddRole()
        //{
        //    AppUser user = await _userManager.FindByNameAsync("Admin");
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");
        //    return Ok("Verildi");
        //}
        
    }
}
