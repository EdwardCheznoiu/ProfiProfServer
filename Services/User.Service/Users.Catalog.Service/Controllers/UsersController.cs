using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using Users.Catalog.Service.Entities;

namespace Users.Catalog.Service.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> userRepository;

        public UsersController(IRepository<User> usersRepository)
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



        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Fname = createUserDto.Fname,
                Lname = createUserDto.Lname,
                Email = createUserDto.Email,
                PhoneNumber = createUserDto.PhoneNumber,
                Function = createUserDto.Function,
                Role = createUserDto.Role
            };
            await userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(PostAsync), new { id = user.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updatedUserDto)
        {
            var currentUser = await userRepository.GetAsync(id);
            if (currentUser == null) return NotFound();

            currentUser.Fname = updatedUserDto.Fname;
            currentUser.Lname = updatedUserDto.Lname;
            currentUser.Email = updatedUserDto.Email;
            currentUser.PhoneNumber = updatedUserDto.PhoneNumber;
            currentUser.Function = updatedUserDto.Function;
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