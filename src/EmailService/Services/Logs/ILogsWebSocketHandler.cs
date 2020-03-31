using System.Net.WebSockets;
using System.Threading.Tasks;

namespace EmailService.Services.Logs
{
    public interface ILogsWebSocketHandler
    {
        Task HandleWebSocketAsync(WebSocket webSocket);
    }
}
