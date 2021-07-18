using System.Threading.Tasks;
using App.Dtos;
using App.Factories;
using App.Services;
using Domain;
using Infra.Database;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : AppControllerBase
    {
        private readonly IUserFactory UserFactory;
        private readonly IResponseFactory ResponseFactory;
        private readonly IUserService UserService;

        public UserController(
            IUnitOfWork unitOfWork, 
            IResponseFactory responseFactory, 
            IUserFactory userFactory,
            IUserService userService)
            : base(unitOfWork) 
        {
            this.UserFactory = userFactory;
            this.ResponseFactory = responseFactory;
            this.UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            User user = this.UserFactory.Create(dto);
            await this.UserService.RegisterUserAsync(user, dto.Password);
            return Ok();
        }
    }
}