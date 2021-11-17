using System.Collections.Generic;
using System.Threading.Tasks;
using App.Services;
using Domain;
using Moq;

namespace Test.Mock
{
    public class UserServiceMock : IObjectMock<IUserService>
    {
        private List<User> Users = new List<User>();

        public IUserService Create()
        {
            var mock = new Mock<IUserService>();
            
            mock.Setup(service => service.RegisterUserAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Callback<User, string>(RegisterUsersAsyncMock);

            return mock.Object;
        }

        private void RegisterUsersAsyncMock(User user, string password) => this.Users.Add(user);
    }
}