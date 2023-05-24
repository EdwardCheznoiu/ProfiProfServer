using Messages.Catalog.Service.Entities;

namespace Messages.Catalog.Service
{
    public static class Extensions
    {
        public static MessageDto AsDto(this Message message)
        {
            return new MessageDto(message.Id, message.UserId, message.MessageBody, message.CreatedDate);
        }
    }
}