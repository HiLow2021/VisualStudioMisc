using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebStudy.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();

        public static string HubUrl => "/" + nameof(NotificationHub); 
        public static IDictionary<string, string> Users => users;

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("接続しました");

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"切断しました。 {e?.Message} {Context.ConnectionId}");

            var id = Context.ConnectionId;

            if (!users.TryGetValue(id, out string username))
            {
                username = "不明";
            }

            users.Remove(id, out string _);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(NotificationMessageType.Receive, username, $"{username} が切断しました。");
            await base.OnDisconnectedAsync(e);
        }

        public async Task SendMessage(string userName, string message)
        {
            await Clients.All.SendAsync(NotificationMessageType.Receive, userName, message);
        }

        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;

            if (!users.ContainsKey(currentId))
            {
                users.TryAdd(currentId, username);
                await Clients.AllExcept(currentId).SendAsync(NotificationMessageType.Receive, username, $"{username} が接続しました。");
            }
        }
    }
}
