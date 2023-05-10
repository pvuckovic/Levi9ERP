using Levi9.ERP.Exceptions;
using Newtonsoft.Json;

namespace Levi9.ERP.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomExceptionBase exception)
            {
                //TODO: log the error
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = exception.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = exception.ErrorMessage }));

            }

        }
    }
}
