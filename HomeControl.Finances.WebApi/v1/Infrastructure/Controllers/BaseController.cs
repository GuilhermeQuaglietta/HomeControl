using HomeControl.Identity.Jwt;
using HomeControl.Identity.Jwt.Web;
using HomeControl.Identity.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeControl.Finances.WebApi.v1.Infrastructure.Controllers
{
    public class BaseController : Controller
    {
        public JwtUser GetUser()
        {
            if (!HttpContext.Items.ContainsKey(JwtHttpKeys.JwtUserItem))
                throw new InvalidOperationException("User token not found");

            return (JwtUser)HttpContext.Items[JwtHttpKeys.JwtUserItem];
        }
    }
}
