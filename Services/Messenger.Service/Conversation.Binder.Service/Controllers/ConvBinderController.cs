using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Conversation.Binder.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Conversation.Binder.Service.Clients;

namespace Conversation.Binder.Service.Controllers
{
    [ApiController]
    [Route("convsbinder")]
    public class ConvBinderController : ControllerBase
    {
        private readonly IRepository<ConvBinder> convBinderRepository;

        private readonly ConversationsClient conversationsClient;

        private readonly MessagesClient messagesClient;

        public ConvBinderController(IRepository<ConvBinder> convBinderRepository, ConversationsClient conversationsClient, MessagesClient messagesClient)
        {
            this.convBinderRepository = convBinderRepository;
            this.conversationsClient = conversationsClient;
            this.messagesClient = messagesClient;
        }

        [HttpGet("{convId}")]
        public async Task<ActionResult<IEnumerable<ConvBinderDto>>> GetAsync(Guid convId)
        {
            var messages = await messagesClient.GetAsync();
            var conversations = await conversationsClient.GetAsync();

            var convBindings = await convBinderRepository.GetAllAsync(conv => conv.ConversationId == convId);

            var convBindedMsgDto = convBindings.Select(binder =>
            {
                var msgs = messages.Single(message => message.Id == binder.MessageId);
                var convs = conversations.Single(conv => conv.Id == binder.ConversationId);
                return binder.AsDto(msgs.MessageBody, msgs.CreatedDate, msgs.UserId, convs.OtherId);
            });
            return Ok(convBindedMsgDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateConvBinderDto createConvBinderDto)
        {
            var convBinder = new ConvBinder
            {
                ConversationId = createConvBinderDto.ConversationId,
                MessageId = createConvBinderDto.MessageId
            };

            await convBinderRepository.CreateAsync(convBinder);

            return Ok("Conversation binder was set");
        }

    }
}