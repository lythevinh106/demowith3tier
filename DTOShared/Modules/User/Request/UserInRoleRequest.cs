using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.User.Request
{
    public class UserInRoleRequest
    {

        [Required]
        public string UserName
        {
            get; set;
        }


        [Required]
        public string RoleName
        {
            get; set;
        }
    }
}
