namespace BodyRevival.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created {  get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }=DateTime.Now;
    }
}
