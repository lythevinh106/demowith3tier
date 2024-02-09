namespace MydemoFirst.Models.Modules.RoomMember.Models
{
    public class RoomMember
    {

        public string RoomId { get; set; }
        public string? UserId { get; set; }


        public virtual User.Models.User? User { get; set; }
        public virtual Room.Models.Room? Room { get; set; }
    }



}
