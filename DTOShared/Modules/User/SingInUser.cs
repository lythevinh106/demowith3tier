using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.User.Models
{
    public class SingInUser
    {



        [Display(Name = "Email")]

        [EmailAddress(ErrorMessage = "{0} Khong dung dinh dang")]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
