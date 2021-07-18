using App.Dtos;
using Domain;

namespace App.Factories
{
    public interface IUserFactory
    {
        User Create(CreateUserDto dto);
    }
}