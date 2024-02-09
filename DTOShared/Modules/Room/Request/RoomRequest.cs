using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.Room.RoomRequest
{
    public class RoomRequest
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }



    }
}
