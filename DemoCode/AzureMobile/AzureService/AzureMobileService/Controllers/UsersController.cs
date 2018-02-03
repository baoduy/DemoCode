using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureMobileService.Entities;
using AzureMobileService.Dal;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureMobileService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly MobileDbContext _dbContext;
        public UsersController(MobileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get() => await _dbContext.Users.ToListAsync();

        [HttpGet("{id}")]
        public async Task<User> Get(int id) => await _dbContext.Users.FindAsync(id);

        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="item"></param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(string.Join(Environment.NewLine, ModelState.SelectMany(m=>m.Value.Errors)));

                if (await _dbContext.Users.AnyAsync(u => string.Equals(u.UserName, item.UserName, StringComparison.CurrentCultureIgnoreCase)))
                    return BadRequest($"UserName {item.UserName} is existed.");

                _dbContext.Users.Add(item);
                var val = await _dbContext.SaveChangesAsync();
                if (val > 0)
                    return Ok(val);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User item)
        {
            try
            {
                if (id != item.Id)
                    return BadRequest("primary key is invalid.");

                if (!ModelState.IsValid)
                    return BadRequest(string.Join(Environment.NewLine, ModelState.SelectMany(m => m.Value.Errors)));
               
                _dbContext.Users.Attach(item);
                _dbContext.Users.Update(item);

                var val = await _dbContext.SaveChangesAsync();
                if (val > 0)
                    return Ok(val);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _dbContext.Users.Remove(await _dbContext.Users.FindAsync(id));
                var val = await _dbContext.SaveChangesAsync();
                if (val > 0)
                    return Ok(val);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ex.Message + Environment.NewLine + ex.InnerException);
            }
        }
    }
}
