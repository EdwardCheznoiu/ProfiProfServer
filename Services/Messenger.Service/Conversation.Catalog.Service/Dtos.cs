using System;
using System.ComponentModel.DataAnnotations;

namespace Conversation.Catalog.Service
{
    public record ConversationDto(Guid Id, Guid UserId, Guid OtherId);
    public record CreateConvDto([Required] Guid UserId, [Required] Guid OtherId);
}