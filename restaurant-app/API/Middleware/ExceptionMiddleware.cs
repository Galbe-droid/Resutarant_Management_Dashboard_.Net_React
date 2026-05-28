namespace Template_restaurant_app.API.Middleware
{
    public class ExceptionMiddleware
    {
        // Middleware to handle exceptions globally and return consistent error responses
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
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ArgumentException => 400,
                KeyNotFoundException => 404,
                _ => 500
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                message = ex.Message,
                statusCode = statusCode
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
