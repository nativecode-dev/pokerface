namespace PokerFace.Web.WebSockets.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models.Poker;
    using Requests;
    using Services;

    public class NewGameHandler : ICancellableAsyncRequestHandler<NewGame, GameModel>
    {
        private readonly IGameService games;

        public NewGameHandler(IGameService games)
        {
            this.games = games;
        }

        public Task<GameModel> Handle(NewGame message, CancellationToken cancellationToken)
        {
            return this.games.NewGameAsync(message.Name, cancellationToken);
        }
    }
}