using Microsoft.AspNetCore.Authorization;

namespace MydemoFirst.Authorization.Requirements
{
    public class CRUDRequirement : IAuthorizationRequirement
    {
        public string Module { get; }
        public string Action { get; }

        public CRUDRequirement(string module, string action)
        {
            Module = module;
            Action = action;
        }
    }
}