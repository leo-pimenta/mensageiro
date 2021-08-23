using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("/contacts")]
    [Authorize]
    public class ContactController : AppControllerBase
    {
        private readonly IResponseFactory ResponseFactory;
        private readonly IContactService ContactService;
        private readonly IContactInvitationFactory ContactInvitationFactory;
        private readonly IContactFactory ContactFactory;

        public ContactController(
            IUnitOfWork unitOfWork,
            IResponseFactory responseFactory,
            IContactService contactService,
            IContactInvitationFactory contactInvitationFactory,
            IContactFactory contactFactory)
            : base(unitOfWork)
        {
            this.ResponseFactory = responseFactory;
            this.ContactService = contactService;
            this.ContactInvitationFactory = contactInvitationFactory;
            this.ContactFactory = contactFactory;
        }

        [HttpPost("invitation")]
        public async Task CreateInvitation(CreateContactInvitationDto dto)
        {
            this.ValidateUser(dto.UserGuid.ToString());
            ContactInvitation invitation = await this.ContactInvitationFactory.Create(dto);
            
            try
            {
                await this.ContactService.RegisterInvitationAsync(invitation);
            }
            catch (UserNotFoundException e)
            {
                throw new BadRequestException(e.Message, e);
            }
        }

        [HttpPost("invitation/accept")]
        public async Task AcceptInvitation(ContactInvitationGuidDto dto)
        {
            try
            {
                ContactInvitation invitation = await this.ContactService.GetInvitationAsync(dto.InvitationGuid);
                this.ValidateUser(invitation.UserGuid.ToString());
                await this.ContactService.AcceptInvitation(invitation);
            }
            catch (ContactInvitationNotFoundException e)
            {
                throw new BadRequestException(e.Message, e);
            }
        }

        [HttpGet("invitations")]
        public async Task<IActionResult> GetAllInvitations(ContactInvitationGuidDto dto)
        {
            IEnumerable<ContactInvitation> invitations = await this.ContactService
                .GetAllInvitationsAsync(dto.InvitationGuid);
            
            IEnumerable<ContactInvitationDto> dtos = invitations.Select(this.ContactInvitationFactory.CreateDto);
            return Ok(this.ResponseFactory.Create(new { invitations = dtos }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guid userGuid = Guid.Parse(this.GetUserIdentifier());
            IList<Contact> contacts = await this.ContactService.GetAllContacts(userGuid);
            IEnumerable<ContactDto> dtos = contacts.Select(c => this.ContactFactory.CreateDto(c));
            return Ok(this.ResponseFactory.Create(new { contacts = dtos }));
        }
    }
}