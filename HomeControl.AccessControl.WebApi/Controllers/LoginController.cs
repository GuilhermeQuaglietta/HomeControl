using HomeControl.AccessControl.Domain.Seedwork;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Login;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserQueries _queries;
        private readonly IUserRepository _repository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IJwtConfiguration _tokenConfiguration;
        private readonly LoginSettings _loginSettings;

        public LoginController(IUserQueries queries,
            IUserRepository repository,
            IJwtHandler jwtHandler,
            IJwtConfiguration tokenConfiguration,
            LoginSettings settings)
        {
            _queries = queries;
            _repository = repository;
            _jwtHandler = jwtHandler;
            _tokenConfiguration = tokenConfiguration;
            _loginSettings = settings;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _queries.LoginUser(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtHandler.GenerateToken(_tokenConfiguration, user.UserId, user.Name, user.Email);

            return Ok(token);
        }

        [HttpPost]
        [Route("recover")]
        public IActionResult SendRecoveryEmail([FromBody]string email)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _queries.FindByEmail(email);

            if (user == null || !user.RecoveryKeyExpired())
                return Ok();

            //TODO adicionar log de tentativa de ataque
            _ = _repository.GenerateRecoveryKey(user.UserId, _loginSettings.RecoverExpirationSeconds);
            return Ok();
        }

        [HttpGet]
        [Route("recover/{recoveryKey}")]
        public IActionResult ValidateRecoveryKey([FromRoute] string recoveryKey)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var userFound = _queries.FindByRecoveryKey(recoveryKey);

            if (userFound == null)
                return new StatusCodeResult((int)HttpStatusCode.Gone);

            return Ok();
        }

        [HttpPut]
        [Route("recover")]
        public IActionResult ChangePassword([FromBody]RecoveryPasswordChangeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _queries.FindByRecoveryKey(request.Recoverykey);

            if (user == null)
                return new StatusCodeResult((int)HttpStatusCode.Gone);

            _repository.ChangePassword(user.UserId, request.NewPassword);

            return Ok();
        }

    }
}