using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HomeControl.Identity.Web
{
    public class JwtAuthorizeFilterAttribute : IActionFilter
    {
        protected string HeaderName { get; set; }
        protected IJwtConfiguration JwtConfiguration { get; set; }
        protected IJwtHandler JwtHandler { get; set; }

        public JwtAuthorizeFilterAttribute(string headerName, IJwtConfiguration jwtConfiguration, IJwtHandler jwtHandler)
        {
            HeaderName = headerName;
            JwtConfiguration = jwtConfiguration;
            JwtHandler = jwtHandler;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var header = context.HttpContext.Request.Headers[HeaderName];

            if (!header.Any())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = header.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var validationResult = JwtHandler.VerifyToken(JwtConfiguration, token);

            if (!validationResult.IsValid)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            JwtUser.SetIdentity(validationResult.Identity);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Nothing
        }
    }
}
