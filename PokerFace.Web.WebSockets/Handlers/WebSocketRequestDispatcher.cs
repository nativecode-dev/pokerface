namespace PokerFace.Web.WebSockets.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Extensions;
    using Extensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;

    public class WebSocketRequestDispatcher : ICancellableAsyncRequestHandler<WebSocketRequest<JObject>, JObject>
    {
        private readonly ILogger logger;

        private readonly IMapper mapper;

        private readonly IMediator mediator;

        public WebSocketRequestDispatcher(ILoggerFactory factory, IMapper mapper, IMediator mediator)
        {
            this.logger = factory.CreateLogger<WebSocketRequestDispatcher>();
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<JObject> Handle(WebSocketRequest<JObject> message, CancellationToken cancellationToken)
        {
            this.logger.LogJson(message);

            var response = new WebSocketResponse {Type = WebSocketResponseType.Reply};

            if (string.IsNullOrWhiteSpace(message.TypeName))
            {
                return response.ToJObject();
            }

            var type = TypeExtensions.GetAppDomainType(message.TypeName);

            if (type.IsMediatorRequest())
            {
                var command = this.mapper.Map(message.Typed ?? new JObject(), typeof(JObject), type);
                response.Data = await this.mediator.Result(command).NoCapture();
            }

            this.logger.LogJson(response);
            return response.ToJObject();
        }
    }
}