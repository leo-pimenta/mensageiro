using System.Security.Claims;
using Infra.Database;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AppControllerBase : ControllerBase
    {
        protected readonly IResponseFactory ResponseFactory;

        public AppControllerBase(IResponseFactory responseFactory)
        {
            this.ResponseFactory = responseFactory;
        }

        protected string GetUserIdentifier()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected void ValidateUser(string expectedIdenfifier)
        {
            if (GetUserIdentifier() != expectedIdenfifier)
            {
                throw new ForbiddenExcepion("The user tried to execute a forbidden action.");
            }
        }
    }
}