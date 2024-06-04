using BodyRevival.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BodyRevival.Areas.Admin.Services
{
    public class LayoutService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public LayoutService(UserManager<AppUser> userManager, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
        }
        public async Task<AppUser> GetUser()
        {
            AppUser user = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);
            return user;
        }
    }
}
