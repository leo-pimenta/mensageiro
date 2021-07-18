using System;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(CreateUserDto dto) 
            => new User()
            {
                Email = dto.Email,
                Guid = Guid.NewGuid(),
                Nickname = dto.Nickname
            };
    }
}