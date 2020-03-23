using HomeControl.Identity.Jwt;
using HomeControl.Identity.Jwt.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeControl.Identity.Web.Filters
{

    public class JwtAuthorizeFilterAttribute : ActionFilterAttribute
    {
        private string _headerName;
        public string HeaderName
        {
            get => !string.IsNullOrWhiteSpace(_headerName) ? _headerName : JwtHttpKeys.TokenAuthorizationHeader;
            set => _headerName = value;
        }

        private string _jwtUserItemName;
        public string JwtUserItemName
        {
            get => !string.IsNullOrWhiteSpace(_jwtUserItemName) ? _jwtUserItemName : JwtHttpKeys.JwtUserItem;
            set => _jwtUserItemName = value;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Headers[HeaderName].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }

            if (token.IndexOf("bearer", StringComparison.InvariantCultureIgnoreCase) > -1)
                token = token.Replace("bearer", string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();

            var provider = context.HttpContext.RequestServices;
            IJwtConfiguration jwtConfiguration = provider.GetService<IJwtConfiguration>();
            IJwtHandler jwtHandler = provider.GetService<IJwtHandler>();
            var validationResult = jwtHandler.VerifyToken(jwtConfiguration, token);

            if (!validationResult.IsValid)
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }

            context.HttpContext.Items.Add(JwtUserItemName, validationResult.Identity);
            return next();
        }
    }
}
