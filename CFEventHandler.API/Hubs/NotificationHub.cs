using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.API.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public override Task OnConnectedAsync()
        {
            System.Diagnostics.Debug.WriteLine("NotificationHub:OnConnectedAsync");
            
            Groups.AddToGroupAsync(Context.ConnectionId, "All");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("NotificationHub:OnDisconnectedAsync");
            
            Groups.RemoveFromGroupAsync(Context.ConnectionId, "All");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
