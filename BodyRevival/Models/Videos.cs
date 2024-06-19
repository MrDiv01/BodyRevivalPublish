using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class Videos:BaseEntity
    {
      
        public string VideoLink { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
    }
}
