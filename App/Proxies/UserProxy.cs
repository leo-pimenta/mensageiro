using System;
using System.Threading.Tasks;
using App.Services;
using Domain;

namespace App.Proxies
{
    public class UserProxy : IProxy<User>
    {
        private readonly IUserService UserService;
        private readonly Guid Guid;
        private User User;

        public UserProxy(Guid guid, IUserService userService)
        {
            this.UserService = userService;
            this.Guid = guid;
        }

        public User Request()
        {
            if (this.User == null)
            {
                this.User = this.UserService.GetUser(this.Guid);
            }

            return this.User;
        }

        public async Task<User> RequestAsync()
        {
            if (this.User == null)
            {
                this.User = await this.UserService.GetUserAsync(this.Guid);
            }

            return this.User;
        }
    }
}