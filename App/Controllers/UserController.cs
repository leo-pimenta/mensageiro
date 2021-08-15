using System.Threading.Tasks;
using App.Auth;
using App.Dtos;
using App.Factories;
using App.Services;
using Domain;
using Infra.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace App.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : AppControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IUserFactory UserFactory;
        private readonly IResponseFactory ResponseFactory;
        private readonly IUserService UserService;
        private readonly ITokenFactory TokenFactory;

        public UserController(
            IConfiguration configuration,
            IUnitOfWork unitOfWork, 
            IResponseFactory responseFactory, 
            IUserFactory userFactory,
            IUserService userService,
            ITokenFactory tokenFactory)
            : base(unitOfWork) 
        {
            this.Configuration = configuration;
            this.UserFactory = userFactory;
            this.ResponseFactory = responseFactory;
            this.UserService = userService;
            this.TokenFactory = tokenFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            User user = this.UserFactory.Create(dto);
            await this.UserService.RegisterUserAsync(user, dto.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            User user = await this.UserService.GetUserAsync(dto.Email);

            if (user == null || !await this.UserService.ValidateLogin(user, dto.Password))
            {
                return Unauthorized();
            }

            string token = this.TokenFactory.Create(user);
            
            return Ok(this.ResponseFactory.Create(new 
            { 
                nickName = user.Nickname,
                accessToken = token 
            }));
        }
    }
}