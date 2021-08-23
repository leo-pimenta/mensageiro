using System.Threading.Tasks;
using App.Dtos;
using Domain;
using Infra.Database.Model;

namespace App.Factories
{
    public interface IContactInvitationFactory
    {
        Task<ContactInvitation> Create(CreateContactInvitationDto dto);
        ContactInvitationDto CreateDto(ContactInvitation invitation);
    }
}