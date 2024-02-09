using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MydemoFirst.Signal.DemoAppChat
{
    public class EmailBaseUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var value = connection.User?.FindFirst(ClaimTypes.Email)?.Value!;

            return value;
        }
    }
}
