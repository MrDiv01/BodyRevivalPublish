using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class Packet:BaseEntity
    {
        public int TeacherId {  get; set; }
        public Teacher Teacher { get; set; }
        public string Name { get; set; }
        public string Titile {  get; set; }
        public string Description {  get; set; }
        public int AttenDance {  get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile {  get; set; }

    }
}
