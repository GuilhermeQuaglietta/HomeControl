using AutoMapper;
using FluentValidation;
using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.Util.ThirdParty.FluentValidation;
using HomeControl.Finances.WebApi.Infrastructure.Validators;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace HomeControl.Finances.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/Account/{accountId}/transaction")]
    public class AccountTransferController : Controller
    {
        protected string ValidationMessage { get; private set; }
        protected readonly IValidator<AccountTransferRequest> Validator;
        protected readonly IAccountTransferRepository Repository;
        protected readonly IMapper Mapper;

        public AccountTransferController(IValidator<AccountTransferRequest> validator, IAccountTransferRepository repository, IMapper mapper)
        {
            Validator = validator ?? throw new ArgumentNullException("validator", "Validator can't be null");
            Repository = repository ?? throw new ArgumentNullException("repository", "Repository can't be null");
            Mapper = mapper ?? throw new ArgumentNullException("mapper", "Mapper can't be null");
        }

        [HttpGet]
        public virtual ActionResult Get(int id)
        {
            var entity = Repository.Get(id);
            return Ok(entity);
        }

        [HttpPost]
        public virtual ActionResult Post(AccountTransferRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ValidateRequest(request, PersistenceValidationTypes.Create))
                return BadRequest(ValidationMessage);

            AccountTransferEntity entity = Mapper.Map<AccountTransferEntity>(request);

            Repository.Insert(entity);
            return Ok(entity);
        }

        [HttpPut]
        public virtual ActionResult Put(AccountTransferRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ValidateRequest(request, PersistenceValidationTypes.Update))
                return BadRequest(ValidationMessage);

            AccountTransferEntity entity = Mapper.Map<AccountTransferEntity>(request);

            Repository.Update(entity);
            return NoContent();
        }

        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            Repository.Delete(id);
            return NoContent();
        }

        protected bool ValidateRequest(AccountTransferRequest entity, string validationType)
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
