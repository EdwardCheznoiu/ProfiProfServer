using System;
using System.ComponentModel.DataAnnotations;

namespace Messages.Catalog.Service
{
    public record MessageDto(Guid Id, Guid UserId, string MessageBody, DateTimeOffset CreatedDate);
    public record CreateMessageDto([Required] Guid UserId, [Required] string MessageBody, DateTimeOffset CreatedDate);
}