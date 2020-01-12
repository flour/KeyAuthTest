using ApiKeyTest.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiKeyTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForAll()
            => Ok("all ok");

        [HttpGet("manager")]
        [Authorize(Roles = Roles.Manager)]
        public IActionResult ForManager()
            => Ok("Hi, manager");

        [HttpGet("employee")]
        [Authorize(Roles = Roles.Employee)]
        public IActionResult ForEmployee()
            => Ok("Hi, employee");

        [HttpGet("accountant")]
        [Authorize(Roles = Roles.Accountant)]
        public IActionResult ForAccountant()
            => Ok("Hi, accountant");

    }
}