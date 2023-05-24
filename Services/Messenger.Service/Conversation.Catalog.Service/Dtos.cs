using System;
using System.ComponentModel.DataAnnotations;

namespace Conversation.Catalog.Service
{
    public record ConversationDto(Guid Id, Guid MessageId, Guid UserId);
    public record CreateConvDto([Required] Guid MessageId, [Required] Guid UserId);
}