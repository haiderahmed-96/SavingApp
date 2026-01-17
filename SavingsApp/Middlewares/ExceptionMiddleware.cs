using System.Net;
using System.Text.Json;

namespace SavingsApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Default
            var statusCode = (int)HttpStatusCode.InternalServerError;

            // نقدر نفرق بعدين بين أنواع أخطاء
            //   Exception من السيرفس تعتبر BadRequest
            statusCode = (int)HttpStatusCode.BadRequest;

            context.Response.StatusCode = statusCode;

            var response = new
            {
                error = ex.Message,
                status = statusCode,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
