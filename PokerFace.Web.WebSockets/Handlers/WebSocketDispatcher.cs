namespace PokerFace.Web.WebSockets.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Linq;

    public class WebSocketDispatcher : ICancellableAsyncRequestHandler<WebSocketRequest<JObject>>
    {
        private readonly IMapper mapper;

        private readonly IMediator mediator;

        private readonly IServiceScope scope;

        public WebSocketDispatcher(IMapper mapper, IMediator mediator, IServiceScopeFactory scopes)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.scope = scopes.CreateScope();
        }

        public Task Handle(WebSocketRequest<JObject> message, CancellationToken token)
        {
            var type = Type.GetType(message.TypeName);
            var instance = (IRequest) this.scope.ServiceProvider.GetService(type);

            this.mapper.Map(message.Typed, instance);

            return this.mediator.Send(instance, token);
        }
    }
}