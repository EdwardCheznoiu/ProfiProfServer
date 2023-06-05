using System.Threading.Tasks;
using Users.Contracts;

namespace Auth.Serivce.src.MessageProcessors
{
    public interface IMessageProcessor
    {
        Task<UserPublishDto> ProcessMessageAsync(UserPublishDto message);
    }
}