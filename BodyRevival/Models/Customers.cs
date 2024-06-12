using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class Customers:BaseEntity
    {
        public string Image {  get; set; }
        [NotMapped]
        public IFormFile ImageFile {  get; set; }
        public string Name {  get; set; }
        public string Description {  get; set; }
    }
}
