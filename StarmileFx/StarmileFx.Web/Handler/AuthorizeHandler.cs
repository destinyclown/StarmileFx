using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StarmileFx.Web.Handler
{
    public class AuthorizeHandler
    {
        
    }
    public class AuthorizeRequirement : IAuthorizationRequirement
    {
    }

    public class HasPasswordHandler : AuthorizationHandler<AuthorizeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Authentication))
                return Task.CompletedTask;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class HasAccessTokenHandler : AuthorizationHandler<AuthorizeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "AccessToken" && c.Issuer == "http://www.cnblogs.com/rohelm"))
                return Task.CompletedTask;

            var toeknExpiryIn = Convert.ToDateTime(context.User.FindFirst(c => c.Type == "AccessToken" && c.Issuer == "http://www.cnblogs.com/rohelm").Value);

            if (toeknExpiryIn > DateTime.Now)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
