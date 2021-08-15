using System.Threading.Tasks;
using Domain;
using Infra.Database;

namespace App.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork UnitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public async Task RegisterInvitationAsync(ContactInvitation invitation)
        {
            await this.UnitOfWork.ExecuteAsync(async context => 
                await context.ContactInvitation.AddAsync(invitation));
        }
    }
}