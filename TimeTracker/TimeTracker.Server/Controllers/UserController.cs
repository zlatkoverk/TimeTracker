using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;

namespace TimeTracker.Server.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public void Register([FromBody] UserCredentials credentials)
        {
            var user = new User() { Credentials = credentials, Id = new Guid() };

            _userRepository.RegisterUser(user);

            return;
        }

        [HttpGet("[action]/{username}")]
        public ActionResult<Response> Free([FromRoute]string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException();
            }
            bool taken = _userRepository.UserExists(username);

            if (taken)
            {
                return new Response("Username is already taken", true);
            }

            return new Response("Username is not taken", false);
        }

        [HttpPost("[action]")]
        public ActionResult<Response> Exists([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.GetUser(credentials);
            if (user != null)
            {
                return new Response("User exists", false);
            }
            return new Response("User does not exist", true);
        }

        [HttpPost("[action]")]
        public ActionResult<User> Login([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.GetUser(credentials);
            return user;
        }
    }
}