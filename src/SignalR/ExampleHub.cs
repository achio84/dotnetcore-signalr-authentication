using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    [Authorize("SignalRPolicy")]
    public class ExampleHub : Hub<IClientContract>
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"New user {Context.ConnectionId} connected.");
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"User {Context.ConnectionId} disconnected.");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToClient(string message)
        {
            await Clients.All.MessageReceived(message);
        }
    }
}
