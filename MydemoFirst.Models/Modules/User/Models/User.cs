using DTOShared.Contracts;
using Microsoft.AspNetCore.Identity;

using System.Collections;

namespace MydemoFirst.Models.Modules.User.Models
{
    public class User : IdentityUser, ISeedable
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;







        public virtual List<MydemoFirst.Models.Modules.Order.Models.Order> Orders { get; set; }
        public virtual UserRefreshToken.Models.UserRefreshToken UserRefreshToken { get; set; }
        public virtual List<RoomMember.Models.RoomMember> RoomMembers { get; set; }
        public IList Seed()
        {
            var Users = new List<User>(
               Enumerable.Range(1, 1).Select(idx => new User
               {
                   Id = "79640b64-94d3-4cb2-89c8-a5fefe3c2051",
                   FirstName = "nguyen",
                   LastName = idx.ToString(),
                   PasswordHash = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
                   Email = $"user{idx}@gmail.com",

               })

               );
            return Users;

        }
    }


    public class UserInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
