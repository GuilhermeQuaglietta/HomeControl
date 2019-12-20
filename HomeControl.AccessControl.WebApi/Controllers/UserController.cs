using AutoMapper;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.WebApi.Requests.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowEverything")]
    public class UserController : Controller
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
        public IActionResult Get(int id)
        {
            var user = _repository.Get(id);
            user.Password = string.Empty;
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Put([FromBody]UserPutRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(request);

            var duplicates = _repository.GetDuplicates(user);
            if (duplicates != null && duplicates.Any())
                return BadRequest("Email e/ou login já foram utilizados.");

            _repository.Insert(user);
            var response = _mapper.Map<UserPutResponse>(user);
            return Ok(response);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult Post(int id, [FromBody]UserPostRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _repository.Get(id);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            _mapper.Map(request, user);

            var duplicates = _repository.GetDuplicates(user);
            if (duplicates != null && duplicates.Any())
                return BadRequest("Email e/ou login já foram utilizados.");

            _repository.Update(user);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _repository.Get(id);

            if (user == null)
                return BadRequest("User not found");

            _repository.Delete(user);
            return Ok("User deleted");
        }
    }
}