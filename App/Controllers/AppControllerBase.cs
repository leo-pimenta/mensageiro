using System.Security.Claims;
using Infra.Database;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AppControllerBase : ControllerBase
    {
        protected IUnitOfWork UnitOfWork { get; }

        public AppControllerBase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
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