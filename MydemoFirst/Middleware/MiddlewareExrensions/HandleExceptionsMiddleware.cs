using MydemoFirst.Errors;
using MydemoFirst.Errors.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MydemoFirst.Middleware.MiddlewareExrensions
{
    public class HandleExceptionsMiddleware
    {
        private readonly RequestDelegate next;
        public HandleExceptionsMiddleware(RequestDelegate next) => this.next = next;


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next(context);

            }



            catch (Exception ex)
            {


                context.Response.ContentType = "application/json";


                var (code, message) = HandleException(context, ex);
                context.Response.StatusCode = code;

                var errorResponse = new ResponseErrors
                {
                    StatusCode = code,
                    Message = ex.Message
                };

                await context.Response.WriteAsync(errorResponse.ToString());

            }




        }

        private (int, string) HandleException(HttpContext context, Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            switch (ex)
            {
                case ExceptionNotFound or FileNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;


                case ExceptionBadRequest or ValidationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case ExceptionForbid:
                    statusCode = StatusCodes.Status403Forbidden;
                    break;

                case ExceptionUnauthorized:
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;


                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;




            }
            return (statusCode, ex.Message);



        }
    }
}
