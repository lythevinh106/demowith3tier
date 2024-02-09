using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MydemoFirst.Errors;

namespace MydemoFirst.Filter
{
    public class ResponseModelCustomFilter : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {


                var apiError = new ResponseModelErrors();
                apiError.StatusCode = 400;
                apiError.Message = new Dictionary<string, List<string>>();
                var errors = context.ModelState.AsEnumerable();
                foreach (var error in errors)
                {
                    if (error.Value?.Errors?.Count > 0)
                    {
                        apiError.Message[error.Key] = new List<string>();

                        foreach (var inner in error.Value.Errors)
                        {
                            apiError.Message[error.Key].Add(inner.ErrorMessage);
                        }

                    }


                }
                context.Result = new BadRequestObjectResult(apiError);
            }




        }
    }
}
