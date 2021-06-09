using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

using Chat.Web.Data;
using Chat.Web.Models;

namespace Chat.Web.Hubs
{
    public class ChatHub: Hub
    {
        public void BroadcastMessage(String username, String message)
        {
            DateTime timestamp = DateTime.Now;

            using (Context context = new Context())
            {
                context.Add(new Message()
                {
                    Text = message,
                    Timestamp = timestamp,
                    Username = username
                });

                Task.WaitAll(this.Clients.All.SendAsync("GetMessage", username, message, timestamp), context.SaveChangesAsync());
            }
        }
    }
}