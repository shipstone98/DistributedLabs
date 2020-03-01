using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat.Web.Hubs
{
    public class ChatHub: Hub
    {
        public async Task BroadcastMessage(String username, String message) => await this.Clients.All.SendAsync("GetMessage", username, message);
    }
}