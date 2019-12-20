
using FluentValidation.Results;
using System.Text;

namespace HomeControl.Finances.Util.ThirdParty.FluentValidation
{
    public static class FluentValidationExtensions
    {

        public static string GetErrorMessages(this ValidationResult result)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in result.Errors)
                sb.AppendLine(item.ErrorMessage);

            return sb.ToString();
        }
    }
}
