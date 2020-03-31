using Models.Logs;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.LogsViewer
{
    class Program
    {
        public const string URL = "";
        public const string KEY = "";
        static async Task Main(string[] args)
        {
            var wsClient = new ClientWebSocket();

            wsClient.Options.SetRequestHeader("Authorization", KEY);
            await wsClient.ConnectAsync(new Uri(URL), CancellationToken.None);

            var array = new byte[4096];
            Console.WriteLine($"Ready to listen\t\t{URL}\n");
            while (true)
            {
                var result = await wsClient.ReceiveAsync(array, CancellationToken.None);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    Console.WriteLine("Not text received");
                    continue;
                }
                var text = Encoding.UTF8.GetString(array, 0, result.Count);
                var logMessage = JsonConvert.DeserializeObject<LogMessage>(text);
                WriteReport(logMessage);
            }
        }

        private static void WriteReport(LogMessage logMessage)
        {
            Console.ForegroundColor = logMessage.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"{logMessage.LogLevel}:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(' ' + logMessage.DateTimeNormalized);
            Console.ResetColor();
            Console.WriteLine($" {logMessage.Category}[{logMessage.EventId.Id}]");
            Console.WriteLine('\t' + logMessage.Message.Replace("\n", "\n\t"));
        }
    }
}
