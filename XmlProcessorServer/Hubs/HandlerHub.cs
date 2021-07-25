using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace XmlProcessorServer.Hubs
{
    public class HandlerHub : Hub
    {
        public Task SendMessageToCaller(string user, string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }
    }
}