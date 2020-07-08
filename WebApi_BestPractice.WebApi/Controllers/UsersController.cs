using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_BestPractice.Common.Exceptions;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Data.Repositories;
using WebApi_BestPractice.Domain.Etities;
using WebApi_BestPractice.Service.Services;
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
        private readonly IJwtService jwtService;

        public UsersController(IUserRepository userRepository, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        [HttpGet("[action]")]
        public async Task<string> GetToken(string userName , string password, CancellationToken cancellationToken) {
            var user = await userRepository.GetUserByUserPassAsync(userName, password, cancellationToken);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwtToken = await jwtService.GenerateAsync(user, cancellationToken);
            return jwtToken;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            
            if (user == null)
                return NotFound();
            
            return Ok(user);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<User>> Post(UserDto userDto, CancellationToken cancellationToken)
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
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult> Put(int id, User user, CancellationToken cancellationToken)
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
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleteUser = await userRepository.GetByIdAsync(cancellationToken, id);
            await userRepository.DeleteAsync(deleteUser, cancellationToken);
            return Ok();
        }

    }
}
