using HomeControl.Identity.Jwt;
using HomeControl.Identity.Web;
using Microsoft.Extensions.Options;

namespace HomeControl.Finances.WebApi.Infrastructure.Filters
{
    public class FinancesAuthorizeFilterAttribute : JwtAuthorizeFilterAttribute
    {
        public FinancesAuthorizeFilterAttribute(IOptions<JwtConfiguration> jwtConfiguration, IJwtHandler jwtHandler) 
            : base("Identity", jwtConfiguration.Value, jwtHandler)
        {
        }
    }
}
