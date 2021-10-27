using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public interface IContactFactory
    {
        Task<Contact> CreateAsync(CreateContactInvitationDto dto, Guid userGuid);
        IEnumerable<Contact> Create(ContactInvitation invitation);
        ContactDto CreateDto(Contact contact);
    }
}