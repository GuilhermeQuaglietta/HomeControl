using AutoMapper;
using FluentValidation;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.Util.ThirdParty.FluentValidation;
using HomeControl.Finances.WebApi.Infrastructure.Validators;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeControl.Finances.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/Account/{accountId}/transaction")]
    public class AccountTransactionController : Controller
    {
        protected string ValidationMessage { get; private set; }
        protected readonly IValidator<AccountTransactionRequest> Validator;
        protected readonly IAccountTransactionRepository Repository;
        protected readonly IMapper Mapper;

        public AccountTransactionController(IValidator<AccountTransactionRequest> validator, IAccountTransactionRepository repository, IMapper mapper)
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public virtual ActionResult Get(int id)
        {
            var entity = Repository.Get(id);
            return Ok(entity);
        }

        [HttpPost]
        public virtual ActionResult Post(AccountTransactionRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            AccountTransactionEntity entity = Mapper.Map<AccountTransactionEntity>(request);

            Repository.Insert(entity);
            return Ok(entity);
        }

        [HttpPut]
        public virtual ActionResult Put(AccountTransactionRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            AccountTransactionEntity entity = Mapper.Map<AccountTransactionEntity>(request);

            Repository.Update(entity);
            return NoContent();
        }

        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            Repository.Delete(id);
            return NoContent();
        }

    }
}
