using BodyRevival.Models;

namespace BodyRevival.ViewModels
{
    public class HomeViewModel
    {
        public List<Teacher> teachers {  get; set; }
        public List<HomeSlider> slider { get; set; }
        public List<Packet> packets { get; set; }
        public List<Lesson> lessons { get; set; }
            
    }
}
