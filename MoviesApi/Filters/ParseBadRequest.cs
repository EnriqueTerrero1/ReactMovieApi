using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MoviesApi.Filters
{
    public class ParseBadRequest : IActionFilter

    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result as IStatusCodeActionResult;
            if (result == null)
            {
                return;
            }
            var statusCode = result.StatusCode;
            if (statusCode == 400)
            {
                var response = new List<string>();
                var badrequestObjectResult = context.Result as BadRequestObjectResult;
                if(badrequestObjectResult.Value is string)
                {
                    response.Add(badrequestObjectResult.Value.ToString());
                }
                else
                {
                    foreach (var key in context.ModelState.Keys)
                    {
                        foreach(var error in context.ModelState[key].Errors)
                        {
                            response.Add($"{key}: {error.ErrorMessage}");
                        }
                    }
                }
                context.Result = new BadRequestObjectResult(response);

            }
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
          
        }
    }
}
