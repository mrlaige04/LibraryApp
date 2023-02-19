namespace Library.API.Middlewares
{
    public class RequestLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLogger(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogger>();
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                var body = "";
                using (StreamReader sr =new StreamReader(context.Request.Body))
                {
                    body = await sr.ReadToEndAsync();
                    
                }
                var loginfo = "";
                loginfo += "\t" + (context.Request.IsHttps ? "HTTPS " : "HTTP ") + " " + context.Request.Protocol + " " + context.Request.Method + " " + context.Request.Path + "\n";
                loginfo += "\t" + "Query: " + context.Request.QueryString + "\n";
                loginfo += "\t" + "Body: " + body + "\n";

                foreach (var item in context.Request.Headers)
                {
                    loginfo += "\t" + "Header: " + item.Key + " " + item.Value + "\n";
                }
                _logger.LogInformation(loginfo);
            }
        }


        
    }

    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogger>();
        }
    }
}
