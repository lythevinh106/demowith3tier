using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.Room.Response
{
    public class RoomResponse
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


    }

    public class RoomsWithCountMemBer
    {
        public int countMember { get; set; }
        public RoomResponse Room { get; set; }

    }
}
