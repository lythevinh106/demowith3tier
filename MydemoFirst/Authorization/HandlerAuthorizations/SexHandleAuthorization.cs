using Microsoft.AspNetCore.Authorization;
using MydemoFirst.Authorization.requirements;

namespace MydemoFirst.Authorization.HandlerAuthorizations
{
    public class SexHandleAuthorization : AuthorizationHandler<SexRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SexRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "sex" && c.Value == ((int)requirement.Sex).ToString()))
            {
                Console.WriteLine("kiem tra gioi tinh thanh cong");
                context.Succeed(requirement);
            }
            else
            {
                Console.WriteLine("kiem tra gioi tinh that bai");
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}