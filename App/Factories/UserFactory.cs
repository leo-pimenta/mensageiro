using System;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(CreateUserDto dto) => new User(Guid.NewGuid(), dto.Email, dto.Nickname);
    }
}