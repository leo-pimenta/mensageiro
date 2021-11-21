using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public interface IContactFactory
    {
        IEnumerable<Contact> Create(ContactInvitation invitation);
        ContactDto CreateDto(Contact contact);
    }
}