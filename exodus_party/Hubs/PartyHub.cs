using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace exodus_party.Hubs
{
    public class PartyHub : Hub
    {
        public async Task PausarVideo(int partyId)
        {
            await Clients.All.SendAsync("ReceberComandoPause", partyId);
        }

        public async Task DarPlayVideo(int partyId)
        {
            await Clients.All.SendAsync("ReceberComandoPlay", partyId);
        }
    }
}
