using System;
using System.Threading.Tasks;
using Auth.Serivce.src;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;

namespace Auth.Serivce.Consumers
{
    public class UserConsumer : IConsumer<UserPublishDto>
    {
        private UserPublishDto userPublishDto;

        public Task Consume(ConsumeContext<UserPublishDto> context)
        {
            if (context.Message != null) userPublishDto = context.Message;
            return Task.CompletedTask;
        }

        public UserPublishDto GetMessage()
        {
            return userPublishDto;
        }

        public Task DeleteMessage()
        {
            userPublishDto = null;
            return Task.CompletedTask;
        }
    }
}