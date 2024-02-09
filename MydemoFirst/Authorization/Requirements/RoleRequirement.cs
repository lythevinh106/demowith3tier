using Microsoft.AspNetCore.Authorization;

namespace MydemoFirst.Authorization.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string role { get; set; }

        public RoleRequirement(string role)
        {
            this.role = role;
        }
    }
}