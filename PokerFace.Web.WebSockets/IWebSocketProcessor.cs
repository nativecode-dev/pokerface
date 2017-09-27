namespace PokerFace.Web.WebSockets
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IWebSocketProcessor
    {
        Task StartAsync(HttpContext context, CancellationToken token = default(CancellationToken));
    }
}