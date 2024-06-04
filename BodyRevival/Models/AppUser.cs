using Microsoft.AspNetCore.Identity;

namespace BodyRevival.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName {  get; set; }

    }
}
