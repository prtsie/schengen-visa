using System.Security.Claims;
using ApplicationLayer.InfrastructureServicesInterfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Common
{
    public class UserIdProvider(IHttpContextAccessor contextAccessor) : IUserIdProvider
    {
        Guid IUserIdProvider.GetUserId()
        {
            var claim = contextAccessor.HttpContext!.User.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim is null)
            {
                throw new InvalidOperationException("UserIdProvider call for request with no authorization");
            }
            return Guid.Parse(claim.Value);
        }
    }
}
