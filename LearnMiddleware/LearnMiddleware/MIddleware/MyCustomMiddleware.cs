
namespace LearnMiddleware.MIddleware
{
    //if you want to separate the custom middle every api
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My Custom Middleware : Before Calling next\r\n");

            await next(context);

            await context.Response.WriteAsync("My Custom Middleware: After Calling next\r\n");
        }
    }
}
