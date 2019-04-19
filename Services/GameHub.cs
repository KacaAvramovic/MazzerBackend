using Microsoft.AspNetCore.SignalR;

namespace WebApi.Services
{
    public class GameHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
        }
    }
}
