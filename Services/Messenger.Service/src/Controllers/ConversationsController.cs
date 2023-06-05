using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Messenger.Service.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Service.Controllers
{
    [ApiController]
    [Route("conversations")]
    public class ConversationsController : ControllerBase
    {
        private readonly IRepository<Conversation> convRepository;

        public ConversationsController(IRepository<Conversation> convRepository)
        {
            this.convRepository = convRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ConversationDto>> GetAsync()
        {
            var conversations = (await convRepository.GetAllAsync()).Select(conv => conv.AsDto());
            return conversations;
        }

        [HttpGet("{userId}, {otherId}")]
        public async Task<ActionResult<IEnumerable<ConversationDto>>> GetByIdsAsync(Guid userId, Guid otherId)
        {
            if (userId == Guid.Empty || otherId == Guid.Empty) return BadRequest();

            var conversations = (await convRepository.GetAllAsync(conv => conv.UserId == userId && conv.OtherId == otherId)).Select(conv => conv.AsDto());
            return Ok(conversations);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateConvDto createConvDto)
        {


            var conversation = new Conversation
            {
                UserId = createConvDto.UserId,
                OtherId = createConvDto.OtherId
            };

            await convRepository.CreateAsync(conversation);
            return Ok("Conversation created");
        }
    }
}