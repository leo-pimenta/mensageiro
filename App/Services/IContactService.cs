using System.Threading.Tasks;
using Domain;

namespace App.Services
{
    public interface IContactService
    {
        Task RegisterInvitationAsync(ContactInvitation invitation);
    }
}