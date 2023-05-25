using System;
using System.ComponentModel.DataAnnotations;

namespace Conversation.Binder.Service
{
    public record ConvBinderDto(Guid Id, Guid ConversationId, Guid MessageId, string MessageBody, DateTimeOffset CreatedDate, Guid UserId, Guid OtherId);
    public record CreateConvBinderDto([Required] Guid ConversationId, [Required] Guid MessageId);

    public record MessageDto(Guid Id, Guid UserId, string MessageBody, DateTimeOffset CreatedDate);
    public record ConversationDto(Guid Id, Guid UserId, Guid OtherId);
}