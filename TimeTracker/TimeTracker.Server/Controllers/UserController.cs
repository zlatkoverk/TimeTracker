using System;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared;
using TimeTracker.Shared.ApiMessages;
using TimeTracker.Shared.Model;

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
        public void Register([FromBody] RegisterModel model)
        {
            var user = new User(model.Name, model.Surname, model.Username, model.Password);

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
        public ActionResult<Response> Exists([FromBody] LoginModel credentials)
        {
            var user = _userRepository.GetUser(new UserCredentials()
            {
                Username = credentials.Username,
                PasswordHash = credentials.Password.GetHashCode()
            });
            if (user != null)
            {
                return new Response("User exists", false);
            }
            return new Response("User does not exist", true);
        }

        [HttpPost("[action]")]
        public ActionResult<User> Login([FromBody] LoginModel credentials)
        {
            var user = _userRepository.GetUser(new UserCredentials()
            {
                Username = credentials.Username,
                PasswordHash = credentials.Password.GetHashCode()
            });
            return user;
        }
    }
}