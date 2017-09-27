namespace PokerFace.Web.WebSockets.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using MediatR;
    using Newtonsoft.Json.Linq;

    public class WebSocketDispatcher : ICancellableAsyncRequestHandler<WebSocketRequest<JObject>, JObject>
    {
        public Task<JObject> Handle(WebSocketRequest<JObject> message, CancellationToken cancellationToken)
        {
            var response = new WebSocketResponse {Type = WebSocketResponseType.Reply};
            var jobject = response.ToJObject();

            return Task.FromResult(jobject);
        }
    }
}