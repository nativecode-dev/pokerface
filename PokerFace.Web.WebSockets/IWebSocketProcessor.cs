namespace PokerFace.Web.WebSockets
{
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWebSocketProcessor
    {
        Task StartAsync(WebSocket socket, CancellationToken token = default(CancellationToken));
    }
}