using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Infrastructure.Settings;
using HomeControl.AccessControl.WebApi.Requests.Login;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserQueries _queries;
        private readonly IJwtHandler _jwtHandler;
        private readonly IOptions<JwtConfiguration> _tokenConfiguration;

        public LoginController(IUserQueries queries, IJwtHandler jwtHandler, IOptions<JwtConfiguration> tokenConfiguration)
        {
            _queries = queries;
            _jwtHandler = jwtHandler;
            _tokenConfiguration = tokenConfiguration;
        }

        public IActionResult Post([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authenticated = _queries.LoginUser(request.Email, request.Password);

            if (!authenticated)
                return BadRequest("Login ou senha inválidos.");

            var token = _jwtHandler.GenerateToken(_tokenConfiguration.Value, request.Email);

            return Ok(token);
        }
    }
}