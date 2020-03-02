using HomeControl.Identity.Jwt;
using HomeControl.Identity.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeControl.Finances.WebApi.v1.Infrastructure.Controllers
{
    public class BaseController : ControllerBase
    {
        public JwtUser GetUser()
        {
            if (!HttpContext.Items.ContainsKey(JwtHttpKeys.JwtUserItem))
                throw new InvalidOperationException("User token not found");

            return (JwtUser)HttpContext.Items[JwtHttpKeys.JwtUserItem];
        }
    }
}
