using System.Collections;
using System.Threading.Tasks;
using Auth.Contracts;
using Common.Repositories;
using MassTransit;
using Users.Catalog.Service.Entities;
using System.Linq;
using Users.Contracts;
using Users.Catalog.Service;
namespace Users.Service.src.Consumers
{
    public class LoginConsumer : IConsumer<LoginPublishDto>
    {
        private readonly IRepository<User> usersRepository;
        private readonly IPublishEndpoint publishEndpoint;
        public LoginConsumer(IRepository<User> usersRepository, IPublishEndpoint publishEndpoint)
        {
            this.usersRepository = usersRepository;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<LoginPublishDto> context)
        {
            var message = context.Message;

            var user = await usersRepository.GetAsync(user => user.Email == message.Email && user.Password == message.Password);

            if (user == null) return;

            var userToSend = new User
            {
                Id = user.Id,
                Fname = user.Fname,
                Lname = user.Lname,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Function = user.Function,
                Cabinet = user.Cabinet,
                ProfileImage = user.ProfileImage,
                Role = user.Role,
                Detalis = user.Detalis
            };
            await publishEndpoint.Publish(userToSend.UserPublishAsDto());
        }
    }
}