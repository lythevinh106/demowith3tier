using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Models.Modules.UserRefreshToken.Models
{
    public class UserRefreshToken
    {
        [Key]
        public string UserId { get; set; }
        public string Token { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }

        public virtual User.Models.User User { get; set; }




    }
}
