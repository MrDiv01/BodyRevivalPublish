using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class Teacher:BaseEntity
    {
        public string? UserId {  get; set; }
        public AppUser User { get; set; }
        public string? Description { get; set; }
        public double? Weight {  get; set; }
        public double? Height {  get; set; }
        public string? Image { get; set; }
        public bool? IsUpdated {  get; set; }
        public string? Speciality {  get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
