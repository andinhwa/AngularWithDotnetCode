using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.Core.Managers;
using WebApp.Core.Models.Identity;

namespace WebApp.API.Controllers {

    [ApiController]
    [AppAuthorize]
    [Route ("api/Users")]
    public class AccountController : ControllerBase {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        public AccountController (
            IAuthManager authManager,
            IMapper mapper
        ) {
            _authManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<UserViewModel>>> Get () {
            var users = await _authManager.GetAllUsersAsync ();
            var result = _mapper.Map<IList<UserViewModel>>(users);
                return Ok (users); 
        }

        [Route ("Get")]
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