using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BodyRevival.Models
{
    public class HomeSlider:BaseEntity
    {
        public string FirstMotivationWord {  get; set; }
        public string SecondMotivationWord { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonText {  get; set; }
        public string Image {  get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
