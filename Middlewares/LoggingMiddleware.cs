using System.Text;

namespace WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = await FormatRequest(context.Request);

            LogToFile("Request", request);

            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    var response = await FormatResponse(context.Response);
                    LogToFile("Response", response);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                LogToFile("Exception", ex.ToString());
                throw;
            }


        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = request.Body;

            using (var reader = new StreamReader(body, Encoding.UTF8, leaveOpen: true))
            {
                var bodyAsText = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return $"Method: {request.Method}, Route: {request.Path}, Body: {bodyAsText}";
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"Status Code: {response.StatusCode}, Body: {text}";
        }

        private void LogToFile(string logType, string message)
        {
            var logPath = @"C:/LogWebApi";
            Directory.CreateDirectory(logPath);

            var logFile = Path.Combine(logPath, $"log_{DateTime.Now:yyyy-MM-dd}.txt");

            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] {message}\n";

            File.AppendAllText(logFile, logMessage);
        }
    }
}
