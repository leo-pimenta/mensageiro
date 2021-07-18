using Infra.Database;
using Infra.Database.Model;
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
    }
}