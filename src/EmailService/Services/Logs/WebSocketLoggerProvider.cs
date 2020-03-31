using Microsoft.Extensions.Logging;

namespace EmailService.Services.Logs
{
    public class WebSocketLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new WebSocketLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
