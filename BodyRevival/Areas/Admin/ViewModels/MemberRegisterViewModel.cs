using System.ComponentModel.DataAnnotations;

namespace BodyRevival.Areas.Admin.ViewModels
{
    public class MemberRegisterViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
