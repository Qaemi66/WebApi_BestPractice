using Microsoft.AspNetCore.Builder;
using WebFramework.Middlewares;

namespace WebApi_BestPractice.WebFramework.Middlewares
{
    public static class CustomExceptionHandlerMiddlewareExtension {
        public static void UseCustomExceptionHandler(this IApplicationBuilder builder) {
            builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
