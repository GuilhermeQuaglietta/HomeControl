using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeControl.AccessControl.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Info()
        {
            string info = $@"
Running!
user: {User.Identity.Name}";

            
            return Ok(info);
        }
    }
}