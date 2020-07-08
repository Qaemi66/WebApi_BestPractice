using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
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
    public class RolesController : ControllerBase
    {
        private readonly IRepository<Role> roleRepository;

        public RolesController(IRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        // GET: api/<RolesController>
        [HttpGet]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<List<Role>>> Get(CancellationToken cancellationToken)
        {
            var roles = await roleRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(roles);
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<Role>> Get(int id, CancellationToken cancellationToken)
        {
            var role = await roleRepository.GetByIdAsync(cancellationToken, id);
            
            if (role==null)
                return NotFound();

            return Ok(role);
        }

        // POST api/<RolesController>
        [HttpPost]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult<Role>> Post(RoleDto roleDto, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = roleDto.Name,
                Description = roleDto.Description,
                Claims = null
            };

            await roleRepository.AddAsync(role, cancellationToken);

            return Ok(role);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult> Put(int id, RoleDto roleDto,  CancellationToken cancellationToken)
        {
            var updateRole = await roleRepository.GetByIdAsync(cancellationToken, id);

            updateRole.Name= roleDto.Name;
            updateRole.Description= roleDto.Description;

            await roleRepository.UpdateAsync(updateRole, cancellationToken);
            return Ok();
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ApiResultFilterAttribute))]
        public async Task<ActionResult> Delete(int id, Role role, CancellationToken cancellationToken)
        {
            var deleteRole = await roleRepository.GetByIdAsync(cancellationToken, id);
            await roleRepository.DeleteAsync(deleteRole, cancellationToken);
            return Ok();
        }
    }
}
