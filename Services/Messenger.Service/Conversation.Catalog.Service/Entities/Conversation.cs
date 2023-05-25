using System;
using Common.Entities;

namespace Conversation.Catalog.Service
{
    public class Conversation : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OtherId { get; set; }
    }
}