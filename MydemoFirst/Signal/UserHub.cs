using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace MydemoFirst.Signal
{
    public class UserHub : Hub
    {
        public static int TotalView { get; set; } = 2;
        public static int TotalUser { get; set; } = 0;

        public async Task NewWindowLoaded(string message)
        {
            TotalView++;

            Log.Information(message);
            await Clients.All.SendAsync("UpdateTotalView", TotalView);
        }

        public override async Task OnConnectedAsync()
        {
            TotalUser++;
            Clients.All.SendAsync("UpdateTotalUser", TotalUser).GetAwaiter().GetResult();

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUser--;
            Clients.All.SendAsync("UpdateTotalUser", TotalUser).GetAwaiter().GetResult();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }




}