using HomeControl.AccessControl.Domain.Seedwork;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Recovery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class RecoveryController : ControllerBase
    {
        private readonly IUserQueries _queries;
        private readonly IUserRepository _repository;
        private readonly LoginSettings _settings;

        public RecoveryController(IUserQueries queries, IUserRepository repository, IOptions<LoginSettings> settings)
        {
            _queries = queries;
            _repository = repository;
            _settings = settings.Value;
        }

        [HttpGet]
        [Route("{recoveryKey}")]
        public IActionResult ValidateRecoveryKey([FromRoute] string recoveryKey)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var userFound = _queries.FindByRecoveryKey(recoveryKey);

            if (userFound == null)
                return new StatusCodeResult((int)HttpStatusCode.Gone);

            return Ok();
        }


        [HttpPost]
        public IActionResult SendRecoveryEmail([FromForm]string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _queries.FindByEmail(email);

            if (user == null)
                return Ok();

            //TODO adicionar log de tentativa de ataque

            string key = _repository.GenerateRecoveryKey(user.UserId, _settings.RecoverExpirationSeconds);
            return Ok();
        }

        [HttpPut]
        public IActionResult ChangePassword([FromForm]RecoveryPasswordChangeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _queries.FindByRecoveryKey(request.Recoverykey);

            if (user == null)
                return BadRequest("Recovery key expired");

            _repository.ChangePassword(user.UserId, request.NewPassword);

            return Ok();
        }
    }
}
