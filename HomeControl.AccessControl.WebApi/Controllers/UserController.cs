using AutoMapper;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Users;
using HomeControl.Finances.WebApi.v1.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get()
        {
            var loggedUser = GetUser();
            var user = _repository.Find(loggedUser.Id);
            user.Password = string.Empty;
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserPostRequest request)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _mapper.Map<User>(request);

            var duplicates = _repository.GetDuplicates(user);
            if (duplicates != null && duplicates.Any())
                return BadRequest("Email e/ou login já foram utilizados.");

            _repository.Insert(user);
            var response = _mapper.Map<UserPostResponse>(user);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]UserPutRequest request)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var loggedUser = GetUser();
            var user = _repository.Find(loggedUser.Id);

            if (user == null)
                return BadRequest("Usuário não encontrado.");

            _mapper.Map(request, user);

            var duplicates = _repository.GetDuplicates(user);
            if (duplicates != null && duplicates.Any())
                return BadRequest("Email e/ou login já foram utilizados.");

            _repository.Update(user);
            return NoContent();
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var user = _repository.Get(id);

        //    if (user == null)
        //        return BadRequest("User not found");

        //    _repository.Delete(user);
        //    return Ok("User deleted");
        //}
    }
}