using Microsoft.AspNetCore.SignalR;
namespace creditcard.webapi.websocket
{
    public class TransactionHub : Hub
    {
        public async Task SendTransaction(string transaction)
        {
            await Clients.All.SendAsync("ReceiveTransaction", transaction);
        }
    }
}
