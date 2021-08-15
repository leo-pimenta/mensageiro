using System.Threading.Tasks;
using App.Dtos;
using Domain;

namespace App.Factories
{
    public interface IContactInvitationFactory
    {
        Task<ContactInvitation> Create(ContactInvitationDto dto);
    }
}