namespace BodyRevival.Models
{
    public class Communication:BaseEntity
    {
        public string Name {  get; set; }
        public string Number { get; set; }
        public string Subject {  get; set; }
        public string Question {  get; set; }
    }
}
