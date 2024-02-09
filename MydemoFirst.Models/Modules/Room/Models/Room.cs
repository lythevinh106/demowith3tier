

using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Models.Modules.Room.Models
{
    public class Room
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public virtual List<RoomMember.Models.RoomMember> RoomMembers { get; set; }

    }





    //public class ListRoomsWithCountMemBer
    //{
    //    public List<RoomsWithCountMemBer> listRoomsWithCountMemBers { get; set; }

    //}


}
