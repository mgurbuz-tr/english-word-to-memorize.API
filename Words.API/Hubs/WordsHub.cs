
using Microsoft.AspNetCore.SignalR;

namespace Words.API.Hubs
{
    public class WordsHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("word", "wind", "rüzgar");
        }
       
    }
}
