using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Managers;
using WebApp.Core.Models.Identity;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AppAuthorize]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        private readonly IAuthManager _authManager;
        public ValuesController (IAuthManager authManager) {

            this._authManager = authManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IList<User>>> Get () {
            var users = await _authManager.GetAllUsersAsync ();
            return Ok (users); //new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
