using System;
using System.Threading.Tasks;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public interface IContactFactory
    {
        Task<Contact> CreateAsync(CreateContactInvitationDto dto, Guid userGuid);
        ContactDto CreateDto(Contact contact);
    }
}