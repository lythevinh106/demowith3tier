using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MydemoFirst.Authorization.Requirements;
using MydemoFirst.Models.Modules.User.Models;
using Serilog;
using System.Security.Claims;

namespace MydemoFirst.Authorization.HandlerAuthorizations
{
    public class RoleHandleAuthorization : AuthorizationHandler<RoleRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<CRUDHandleAuthorization> _logger;

        public RoleHandleAuthorization(

           ILogger<CRUDHandleAuthorization> logger,
           UserManager<User> userManager,
             RoleManager<IdentityRole> roleManager
           )
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {




            if (await isHasRole(requirement, context))
            {
                context.Succeed(requirement);
            }
            else
            {
                Log.Error("Xác thực  Role Crud thất bại");
                context.Fail();
            }

            await Task.CompletedTask;
        }


        private async Task<bool> isHasRole(RoleRequirement? requirement, AuthorizationHandlerContext context)
        {
            bool existRole = false;

            Claim emailClaim = context.User.FindFirst(ClaimTypes.Email);


            if (emailClaim != null)
            {
                string email = emailClaim.Value.ToString();

                if (!email.IsNullOrEmpty())
                {
                    User user = await _userManager.FindByEmailAsync(email);

                    var rolesNameFromUser = await _userManager.GetRolesAsync(user);
                    if (rolesNameFromUser != null)
                        existRole = rolesNameFromUser.Any(rolenName => rolenName.Equals(requirement.role));



                }
            }

            return existRole;
        }


    }



}
