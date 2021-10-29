using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [ApiController]
    [Route("/groups")]
    [Authorize]
    public class GroupController : AppControllerBase
    {
        private readonly IChatGroups ChatGroups;

        public GroupController(IResponseFactory responseFactory, IChatGroups chatGroups) : base(responseFactory) => 
            this.ChatGroups = chatGroups;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guid userId = Guid.Parse(this.GetUserIdentifier());
            IEnumerable<ChatGroup> groups = await this.ChatGroups.GetAllByUserId(userId).ToListAsync();
            return Ok(this.ResponseFactory.Create(new { groups }));
        }
    }
}