using AutoMapper;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.WebApi.Infrastructure.Filters;
using HomeControl.Finances.WebApi.v1.Infrastructure.Controllers;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using Microsoft.AspNetCore.Mvc;

namespace HomeControl.Finances.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/AccountType")]
    [FinancesAuthorizeFilter]
    public class AccountTypeController : BaseController
    {
        protected readonly IAccountTypeRepository Repository;
        protected readonly IMapper Mapper;

        public AccountTypeController(IAccountTypeRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public virtual ActionResult Get()
        {
            var user = GetUser();
            var entity = Repository.GetAll(user.Id);
            return Ok(entity);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual ActionResult Get(int id)
        {
            var user = GetUser();
            var entity = Repository.Get(id, user.Id);
            return Ok(entity);
        }

        [HttpPost]
        public virtual ActionResult Post(AccountTypeRequest request)
        {
            var user = GetUser();
            request.OwnerId = user.Id;

            AccountTypeEntity entity = Mapper.Map<AccountTypeEntity>(request);
            Repository.Insert(entity, user.Id);
            return Ok(entity);
        }

        [HttpPut]
        public virtual ActionResult Put(AccountTypeRequest request)
        {

            AccountTypeEntity entity = Mapper.Map<AccountTypeEntity>(request);

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
