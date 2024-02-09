using DTOShared.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MydemoFirst.Authorization.requirements
{
    public class SexRequirement : IAuthorizationRequirement
    {
        public Sex? Sex { get; set; }

        public SexRequirement(Sex sex)
        {
            Sex = sex;
        }
    }
}