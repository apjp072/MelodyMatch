using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, //"whats going to happen after I've done my part"
        ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment env) //debug or production
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        //this piece of middleware will catch any exception that isn't handled by other middleware
        public async Task InvokeAsync(HttpContext context)  //have to use InvokeAsync 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json"; //returning something to the client
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() //Sends stack trace in dev mode, "Internal Server Error" if in production mode
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) //? in StackTrace sigifies optional
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");
            
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                
                var json = JsonSerializer.Serialize(response,options); //Converts the provided value to UTF-8 encoded JSON text and write it to the System.IO.Stream.
            
                await context.Response.WriteAsync(json); //returns the above as an HTTP response when we've encountered an error
            }
        }
    }

}

