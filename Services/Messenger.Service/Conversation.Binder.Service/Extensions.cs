using System;
using Conversation.Binder.Service.Entities;

namespace Conversation.Binder.Service
{
    public static class Extenions
    {
        public static ConvBinderDto AsDto(this ConvBinder convBinder, string MessageBody, DateTimeOffset CreatedDate, Guid UserId, Guid OtherId)
        {
            return new ConvBinderDto(convBinder.Id, convBinder.ConversationId, convBinder.MessageId, MessageBody, CreatedDate, UserId, OtherId);
        }
    }
}