using HomeControl.Identity.Jwt;
using HomeControl.Identity.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace HomeControl.Finances.WebApi.Infrastructure.Filters
{
    public class FinancesAuthorizeFilterAttribute : JwtAuthorizeFilterAttribute
    {

    }
}
