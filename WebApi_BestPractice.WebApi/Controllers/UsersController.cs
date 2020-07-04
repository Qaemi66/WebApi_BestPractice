using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Domain.Etities;
using WebApi_BestPractice.WebApi.Models;
using WebFramework.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_BestPractice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [ApiResultFilter]
        public async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [ApiResultFilter]
        public async Task<ActionResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ApiResultFilter]
        public async Task<ActionResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                PasswordHash = SecurityHelper.GetSha256Hash(userDto.Password),
                Age = userDto.Age
            };

            await userRepository.AddAsync(user, userDto.Password, cancellationToken);
            return Ok(user);
        }

        [HttpPut]
        [ApiResultFilter]
        public async Task<ActionResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.UserName = user.UserName;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.FullName = user.FullName;
            updateUser.Age = user.Age;
            updateUser.Gender = user.Gender;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = user.LastLoginDate;


            await userRepository.UpdateAsync(updateUser, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ApiResultFilter]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleteUser = await userRepository.GetByIdAsync(cancellationToken, id);
            await userRepository.DeleteAsync(deleteUser, cancellationToken);
            return Ok();
        }

    }
}
