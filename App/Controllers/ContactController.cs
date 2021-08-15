using System;
using System.Threading.Tasks;
using App.Dtos;
using App.Factories;
using App.Services;
using Domain;
using Infra.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("/contact")]
    [Authorize]
    public class ContactController : AppControllerBase
    {
        private readonly IContactService ContactService;
        private readonly IContactInvitationFactory ContactInvitationFactory;
        private readonly IContactFactory ContactFactory;

        public ContactController(
            IUnitOfWork unitOfWork,
            IContactService contactService,
            IContactInvitationFactory contactInvitationFactory,
            IContactFactory contactFactory)
            : base(unitOfWork)
        {
            this.ContactService = contactService;
            this.ContactInvitationFactory = contactInvitationFactory;
            this.ContactFactory = contactFactory;
        }

        [HttpPost("invitation")]
        public async Task Create(ContactInvitationDto dto)
        {
            ContactInvitation invitation = await this.ContactInvitationFactory.Create(dto);
            await this.ContactService.RegisterInvitationAsync(invitation);
        }
    }
}