using System;
using Common.Entities;

namespace Conversation.Binder.Service.Entities
{
    public class ConvBinder : IEntity
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid MessageId { get; set; }
    }
}