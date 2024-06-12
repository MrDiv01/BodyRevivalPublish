using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class Blog:BaseEntity
    {
        public string Image {  get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public DateTime OrganizationTime { get; set; }
    }
}
