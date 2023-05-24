namespace Conversation.Catalog.Service
{
    public static class Extensions
    {
        public static ConversationDto AsDto(this Conversation conversation)
        {
            return new ConversationDto(conversation.Id, conversation.MessageId, conversation.UserId);
        }
    }
}