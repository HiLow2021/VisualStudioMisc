using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using WebStudy.Hubs;

namespace WebStudy.Data
{
    public class HubClient : IAsyncDisposable
    {
        private static string HubUrl => NotificationHub.HubUrl;
        private HubConnection _HubConnection;

        public string UserName { get; }
        public string Url { get; }
        public bool IsConnected => _HubConnection?.State == HubConnectionState.Connected;

        public event EventHandler<RecievedEventArgs> Recieved;

        public HubClient(string userName, string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(userName)) { throw new ArgumentNullException(nameof(userName)); }
            if (string.IsNullOrWhiteSpace(baseUrl)) { throw new ArgumentNullException(nameof(baseUrl)); }

            UserName = userName;
            Url = baseUrl.TrimEnd('/') + HubUrl;
        }

        public async Task StartAsync()
        {
            if (IsConnected)
            {
                return;
            }

            _HubConnection = new HubConnectionBuilder()
                .WithUrl(Url)
                .WithAutomaticReconnect()
                .Build();
            _HubConnection.On<string, string>(nameof(NotificationMessageType.Receive), (userName, message) =>
            {
                Recieved?.Invoke(this, new RecievedEventArgs(userName, message));
            });

            await _HubConnection?.StartAsync();
            await RegisterAsync();
        }

        public async Task StopAsync()
        {
            if (!IsConnected)
            {
                return;
            }

            await _HubConnection?.StopAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await StopAsync();
            await _HubConnection?.DisposeAsync();

            _HubConnection = null;
        }

        public Task SendAsync(string message) => _HubConnection?.SendAsync(nameof(NotificationHub.SendMessage), UserName, message);
        private Task RegisterAsync() => _HubConnection?.SendAsync(nameof(NotificationHub.Register), UserName);
    }
}
