using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace EvaluationFormsManager.WebApi.Middleware
{
    public class ValidateUserIdAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var arguments = context.ActionArguments;
            var noUserProvidedResult = new BadRequestObjectResult(
                ErrorsDictionary.GetResultObject(ErrorCodes.ERR_USER_ID_INVALID));
            const string userIdKey = "userId";

            if (!arguments.ContainsKey(userIdKey))
            {
                context.Result = noUserProvidedResult;
                return;
            }

            await next();
        }
    }
}
