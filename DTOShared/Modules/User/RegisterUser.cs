using DTOShared.Enums;
using DTOShared.Validation;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.User
{
    public class RegisterUser
    {

        [Display(Name = "Ho")]
        [StringLength(200, ErrorMessage = "{0} k dc dai qua  200 ki tu")]
        [Required(ErrorMessage = "{0} phai nhap ")]

        public string FirstName { get; set; }


        [Display(Name = "Ten")]
        [StringLength(200, ErrorMessage = "{0} k dc dai qua  200 ki tu")]
        [Required(ErrorMessage = "{0} phai nhap ")]

        public string LastName { get; set; }




        [Display(Name = "Giới Tính")]
        [Required(ErrorMessage = "{0} phai Có ")]
        [CheckSex]
        public Sex? Sex { get; set; }



        [Display(Name = "Email")]

        [EmailAddress(ErrorMessage = "{0} Khong dung dinh dang")]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }



    }
}
