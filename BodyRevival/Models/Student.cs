namespace BodyRevival.Models
{
    public class Student : BaseEntity
    {
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public int PacketId { get; set; }
        public Packet Packet { get; set; }
        public int? currentAttendance { get; set; }
        public bool? İsParticipates { get; set; }
    }
}
