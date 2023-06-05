using System.Threading.Tasks;
using Auth.Contracts;
using Auth.Serivce.Consumers;
using Auth.Serivce.src;
using Auth.Serivce.src.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Common.Tokens;
using Common.Repositories;
using Auth.Serivce.Entities;
using System;
using System.Linq;
using Common.Models;

namespace Auth.Serivce.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly UserConsumer userConsumer;
        private readonly UserToken userToken;
        private readonly IRepository<Login> loginRepository;
        public AuthController(IPublishEndpoint publishEndPoint, UserConsumer userConsumer, UserToken userToken, IRepository<Login> loginRepository)
        {
            this.publishEndpoint = publishEndPoint;
            this.userConsumer = userConsumer;
            this.userToken = userToken;
            this.loginRepository = loginRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CreateLoginDto createLoginDto)
        {
            if (createLoginDto.Email == null || createLoginDto.Password == null) return BadRequest();
            var login = new LoginModel
            {
                Email = createLoginDto.Email,
                Password = Utilities.HashPassword(createLoginDto.Password)
            };

            await publishEndpoint.Publish(new LoginPublishDto(login.Email, login.Password));
            var message = userConsumer.GetMessage();
            if (message != null)
            {
                var userLogin = new Login
                {
                    UserId = message.Id,
                    CreatedDate = DateTimeOffset.UtcNow
                };
                await loginRepository.CreateAsync(userLogin);
                var logins = await loginRepository.GetAllAsync(login => login.UserId == message.Id);

                var userToSendDto = new DataToSend
                {
                    UserId = message.Id,
                    Fname = message.Fname,
                    Lname = message.Lname,
                    Email = message.Email,
                    PhoneNumber = message.PhoneNumber,
                    Function = message.Function,
                    Cabinet = message.Cabinet,
                    ProfileImage = message.ProfileImage,
                    Role = message.Role,
                    Details = message.Details,
                    LastLogin = logins.Last().CreatedDate,
                    LoginTimes = logins.Count()
                };
                await userConsumer.DeleteMessage();
                return Ok(userToken.GenerateToken(userToSendDto));
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateRegisterDto createRegisterDto)
        {
            var register = new RegisterModel
            {
                Fname = createRegisterDto.Fname,
                Lname = createRegisterDto.Lname,
                Email = createRegisterDto.Email,
                Password = Utilities.HashPassword(createRegisterDto.Password),
                PhoneNumber = createRegisterDto.PhoneNumber,
                Role = createRegisterDto.Role
            };
            await publishEndpoint.Publish(new RegisterPublishDto(register.Fname, register.Lname, register.Email, register.Password, register.PhoneNumber, register.Role));
            return Ok();
        }
    }
}