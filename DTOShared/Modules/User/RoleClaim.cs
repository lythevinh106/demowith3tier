using DTOShared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.User.Models
{
    public class RoleClaims
    {

        [Required]
        public string RoleId { get; set; }

        [DisplayName("ListClaim")]
        public List<ClaimsInRole>? Claims { get; set; }
    }



    public class ClaimsInRole
    {


        [DisplayName("Permission")]
        [CheckListModule]
        public string? Module { get; set; }

        [DisplayName("Permission")]
        [CheckListCRUD]
        public List<string>? Permissions { get; set; }
    }
}
