using AutoMapper;
using FluentValidation;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.Util.ThirdParty.FluentValidation;
using HomeControl.Finances.WebApi.Infrastructure.Validators;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeControl.Finances.WebApi.v1.Controllers
{
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    [Route("api/v1/Account")]
    public class AccountController : Controller
    {
        protected string ValidationMessage { get; private set; }
        protected readonly IValidator<AccountRequest> Validator;
        protected readonly IAccountRepository Repository;
        protected readonly IMapper Mapper;

        public AccountController(AbstractValidator<AccountRequest> validator, IAccountRepository repository, IMapper mapper)
        {
            Validator = validator ?? throw new ArgumentNullException("validator", "Validator can't be null");
            Repository = repository ?? throw new ArgumentNullException("repository", "Repository can't be null");
            Mapper = mapper ?? throw new ArgumentNullException("mapper", "Mapper can't be null");
        }

        [HttpGet]
        public virtual ActionResult Get()
        {
            var entity = Repository.GetAll();
            return Ok(entity);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual ActionResult Get(int id)
        {
            var entity = Repository.Get(id);
            return Ok(entity);
        }

        [HttpPost]
        public virtual ActionResult Post(AccountRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ValidateRequest(request, PersistenceValidationTypes.Create))
                return BadRequest(ValidationMessage);

            AccountEntity entity = Mapper.Map<AccountEntity>(request);

            Repository.Insert(entity);
            return Ok(entity);
        }

        [HttpPut]
        public virtual ActionResult Put(AccountRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ValidateRequest(request, PersistenceValidationTypes.Update))
                return BadRequest(ValidationMessage);

            AccountEntity entity = Mapper.Map<AccountEntity>(request);

            Repository.Update(entity);
            return NoContent();
        }

        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            Repository.Delete(id);
            return NoContent();
        }

        protected bool ValidateRequest(AccountRequest entity, string validationType)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "Entity can't be null");

            var validationResult = Validator.Validate(entity);

            if (!validationResult.IsValid)
                ValidationMessage = validationResult.GetErrorMessages();

            return validationResult.IsValid;
        }
    }
}
