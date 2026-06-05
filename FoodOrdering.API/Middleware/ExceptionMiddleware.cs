using System.Net;
using System.Text.Json;
using FoodOrdering.Common.Models;

namespace FoodOrdering.API.Middleware
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
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                var response = new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };

                var result = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(result);
            }
        }
    }
}