using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers
{
    /// Base controller class for api controllers in project
    public abstract class VisaApiControllerBase : ControllerBase
    {
        /// Returns identifier of authenticated user
        protected Guid GetUserId() => Guid.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
