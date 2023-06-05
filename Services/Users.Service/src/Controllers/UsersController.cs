using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Users.Catalog.Service.Entities;

namespace Users.Catalog.Service.Controllers
{
    [ApiController]
    [Route("users")]

    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> userRepository;


        public UsersController(IRepository<User> usersRepository, IPublishEndpoint publishEndpoint)
        {
            this.userRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            var users = (await userRepository.GetAllAsync()).Select(user => user.AsDto());
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = (await userRepository.GetAsync(id));
            if (user == null) return NotFound();
            return user.AsDto();
        }

        [HttpGet("entity/{name}/{role}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByNameAsync(string name, string role)
        {
            var users = (await userRepository.GetAllAsync());
            if (users == null) return BadRequest();
            var usersByRole = users.Where(user => (name.Contains(user.Fname) || name.Contains(user.Lname)) && user.Role == role).Select(user => user.AsDto());
            return Ok(usersByRole);
        }

        [HttpGet("entity/{role}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByRoleAsync(string role)
        {
            var users = (await userRepository.GetAllAsync());
            if (users == null) return BadRequest();
            var usersByRole = users.Where(user => user.Role == role).Select(user => user.AsDto());
            return Ok(usersByRole);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Fname = createUserDto.Fname,
                Lname = createUserDto.Lname,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                PhoneNumber = createUserDto.PhoneNumber,
                Role = createUserDto.Role
            };
            await userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(PostAsync), new
            {
                id = user.Id
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updatedUserDto)
        {
            var currentUser = await userRepository.GetAsync(id);
            if (currentUser == null) return NotFound();

            currentUser.Fname = updatedUserDto.Fname;
            currentUser.Lname = updatedUserDto.Lname;
            currentUser.Email = updatedUserDto.Email;
            currentUser.Password = updatedUserDto.Password;
            currentUser.PhoneNumber = updatedUserDto.PhoneNumber;
            currentUser.Function = updatedUserDto.Function;
            currentUser.Cabinet = updatedUserDto.Cabinet;
            currentUser.ProfileImage = updatedUserDto.ProfileImage;
            currentUser.Role = updatedUserDto.Role;

            await userRepository.UpdateAsync(currentUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null) return NotFound();

            await userRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}