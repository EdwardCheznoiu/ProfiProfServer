using System.Threading.Tasks;
using Users.Contracts;

namespace Auth.Serivce.src.MessageProcessors
{
    public class MessageProcessor : IMessageProcessor
    {
        public async Task<UserPublishDto> ProcessMessageAsync(UserPublishDto message)
        {
            // Process the message and obtain the desired result or outcome
            //var result = await SomeProcessingLogic.ProcessAsync(message);

            return message;
        }
    }
}