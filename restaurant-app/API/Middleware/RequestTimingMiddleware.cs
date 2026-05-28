using Serilog;
using System.Diagnostics;

namespace Template_restaurant_app.API.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                Log.Warning(
                    "Slow Request: {Method} {Path} took {Elapsed} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds
                );
            }

            Log.Information(
                "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.Elapsed.TotalMilliseconds
            );
        }
    }
}
