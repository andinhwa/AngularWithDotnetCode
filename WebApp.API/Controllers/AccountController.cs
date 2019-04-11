using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Managers;
using WebApp.Core.Models.Identity;

namespace WebApp.API.Controllers {

    [ApiController]
    [AppAuthorize]
     [Route ("api/User")]
    public class AccountController : ControllerBase {
        private readonly IAuthManager _authManager;

        public AccountController (IAuthManager authManager) {
            this._authManager = authManager;
        }

        [Route ("GetAll")]
        public async Task<ActionResult> GetAllUsers () {
            return Ok (await _authManager.GetAllUsersAsync ());
        }
[       
        Route ("Get")]
        public async Task<ActionResult> Get (Guid id) {
            return Ok (await _authManager.FindUserByIdAsync (id));
        }

        [HttpPost]
        [Route ("Add")]
        public async Task<ActionResult> Add ([FromBody] string value) {
            var user = value.JsonDeserializeObject<User> ();
            var result = await _authManager.CreateAsync (user, "");
            return Ok (result.Succeeded);
        }
    }
}