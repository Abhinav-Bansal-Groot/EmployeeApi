using System.Diagnostics;

namespace EmployeeApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        { 
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine($"HTTP {context.Request.Method} {context.Request.Path}");
            await _next(context);
            sw.Stop();
            Console.WriteLine($"HTTP {context.Response.StatusCode} responded in {sw.ElapsedMilliseconds}ms");
        }
    }
}
