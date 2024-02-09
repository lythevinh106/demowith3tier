using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MydemoFirst.Authorization.Requirements;
using MydemoFirst.Models.Modules.User.Models;
using Serilog;
using System.Security.Claims;

namespace MydemoFirst.Authorization.HandlerAuthorizations
{
    public class CRUDHandleAuthorization : AuthorizationHandler<CRUDRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<CRUDHandleAuthorization> _logger;

        public CRUDHandleAuthorization(

            ILogger<CRUDHandleAuthorization> logger,
            UserManager<User> userManager,
              RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CRUDRequirement requirement)
        {
            Log.Information("RUDHandleAuthorization----");

            if (await isHasRoleClaim(requirement, context))
            {
                context.Succeed(requirement);
            }
            else
            {
                Log.Error("Xác thực  Role Crud thất bại");
                context.Fail();
            }
        }

        private async Task<bool> isHasRoleClaim(CRUDRequirement? crudRequirement, AuthorizationHandlerContext context)
        {
            bool existModuleAndAction = false;

            Claim emailClaim = context.User.FindFirst(ClaimTypes.Email);
            if (emailClaim != null)
            {
                string email = emailClaim.Value.ToString();

                if (!email.IsNullOrEmpty())
                {
                    User user = await _userManager.FindByEmailAsync(email);

                    var rolesNameFromUser = await _userManager.GetRolesAsync(user);

                    if (rolesNameFromUser != null && rolesNameFromUser.Count > 0)
                    {
                        List<IdentityRole> roles = new List<IdentityRole>();

                        foreach (var roleName in rolesNameFromUser)
                        {
                            var role = await _roleManager.FindByNameAsync(roleName);

                            roles.Add(role);
                        }

                        if (roles.Count > 0)
                        {
                            foreach (var role in roles)
                            {
                                var listClaimOfRole = await _roleManager.GetClaimsAsync(role);

                                existModuleAndAction = listClaimOfRole
                                .Any(claimOfRole => claimOfRole.Type.Contains(crudRequirement?.Module) &&
                                claimOfRole.Value.Contains(crudRequirement?.Action)
                                );
                                if (existModuleAndAction) break;
                            }
                        }
                    }
                }
            }

            return existModuleAndAction;
        }
    }
}