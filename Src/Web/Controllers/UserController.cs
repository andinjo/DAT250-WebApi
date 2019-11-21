using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Core;
using Services.ClientWrappers;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserClientWrapper _userClient;

        public UserController(IUserClientWrapper userClient)
        {
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> Read(string userId)
        {
            var user = await _userClient.Get(userId);

            return Ok(user);
        }
    }
}
