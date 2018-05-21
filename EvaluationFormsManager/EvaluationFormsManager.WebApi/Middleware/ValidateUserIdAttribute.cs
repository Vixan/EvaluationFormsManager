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
            var noUserProvidedResult = new JsonResult(new { status = 400, error = "Invalid provided User Identifier." });
            const string userIdKey = "userId";

            if (!arguments.ContainsKey(userIdKey))
            {
                context.Result = noUserProvidedResult;
                return;
            }

            var userId = arguments[userIdKey];

            if (!int.TryParse(userId.ToString(), out int internalUserId))
            {
                context.Result = noUserProvidedResult;
                return;
            }

            await next();
        }
    }
}
