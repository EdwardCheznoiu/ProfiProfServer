using System;
using Common.Entities;

namespace Conversation.Catalog.Service
{
    public class Conversation : IEntity
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
    }
}