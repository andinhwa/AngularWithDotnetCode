using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Managers;
using WebApp.Core.Models.Identity;
using WebApp.DataStructure;

namespace WebApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [AppAuthorize]
    public class CustomerController : ControllerBase {
        // GET api/values
        private readonly ICustomerManager _customerManager;
        public CustomerController (ICustomerManager customerManager) {

            this._customerManager = customerManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IList<CustomerViewModel>>> Get () {
            var users = await _customerManager.GetAllAsync ();
            return Ok (users);
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<CustomerViewModel>> Get (Guid id) {
            return await _customerManager.GetByIdAsync (id);
        }

        // POST api/values
        [HttpPost]
        public async Task<CustomerViewModel> Post (CustomerViewModel customer) {
           // var customer = value.JsonDeserializeObject<CustomerViewModel> ();
           return  await _customerManager.AddOrUpdateAsync (customer);
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public async Task Put (int id, [FromBody] string value) {
            var customer = value.JsonDeserializeObject<CustomerViewModel> ();
            await _customerManager.UpdateAsync (customer);
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public async Task Delete (Guid id) {
            await _customerManager.DeleteAsync (id);
        }

        [HttpGet]
        [Route ("search")]
        public async Task<ActionResult<IList<CustomerViewModel>>> Search (string key) {
            var users = await _customerManager.SearchAsync (key);
            return Ok (users);
        }
    }
}