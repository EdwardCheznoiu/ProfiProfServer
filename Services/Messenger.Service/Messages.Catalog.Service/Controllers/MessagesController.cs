using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Messages.Catalog.Service.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Messages.Catalog.Service.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IRepository<Message> messageRepository;

        public MessagesController(IRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageDto>> GetAsync()
        {
            var messages = (await messageRepository.GetAllAsync()).Select(message => message.AsDto());
            return messages;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest();
            var messages = (await messageRepository.GetAllAsync(message => message.UserId == userId)).Select(message => message.AsDto());
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateMessageDto createMessageDto)
        {
            var message = new Message
            {
                UserId = createMessageDto.UserId,
                MessageBody = createMessageDto.MessageBody,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await messageRepository.CreateAsync(message);

            return Ok("Message created");
        }
    }
}