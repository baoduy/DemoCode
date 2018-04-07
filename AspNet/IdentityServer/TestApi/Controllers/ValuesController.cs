using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ValuesController : Controller
    {
        public ValuesController()
        { }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get() => new string[] { "value1", "value2" };

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "ApiRead")]
        public string GetById(int id)
        {
            return $"value with Id {id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
