using Microsoft.AspNetCore.SignalR;

namespace JoimChat.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            Console.WriteLine("Message sent from ChatHub");
        }
    }
}
