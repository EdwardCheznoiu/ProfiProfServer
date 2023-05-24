using System;
using Common.Entities;

namespace Messages.Catalog.Service.Entities
{
    public class Message : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MessageBody { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}