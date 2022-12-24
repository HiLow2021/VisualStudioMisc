using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using WebStudy.Hubs;

namespace HubClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("URLを入力してください。");

            var url = Console.ReadLine();
            var hubConnection = new HubConnectionBuilder().WithUrl($"https://{url}/NotificationHub").WithAutomaticReconnect().Build();

            hubConnection.On<string, string>(nameof(NotificationMessageType.Receive), (userName, message) =>
            {
                Console.WriteLine($"{userName} : {message}");
            });

            Task.Run(async () =>
            {
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync(nameof(NotificationMessageType.Register), "ConsoleClient_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                Console.WriteLine("入力を受け付けると終了します。");
                Console.ReadLine();

                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
            }).Wait();
        }
    }
}
