namespace PokerFace.Services.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models.Poker;
    using Requests;

    public class GameCommandHandler :
        ICancellableAsyncRequestHandler<CompleteGame, CompletedGameModel>,
        ICancellableAsyncRequestHandler<JoinGame, PlayerModel>,
        ICancellableAsyncRequestHandler<NewGame, GameModel>
    {
        private readonly IGameService games;

        public GameCommandHandler(IGameService games)
        {
            this.games = games;
        }

        public Task<GameModel> Handle(NewGame message, CancellationToken cancellationToken)
        {
            return this.games.NewGameAsync(default(string), cancellationToken);
        }

        public Task<PlayerModel> Handle(JoinGame message, CancellationToken cancellationToken)
        {
            return this.games.JoinedAsync(message.GameId, message.PlayerName, cancellationToken);
        }

        public Task<CompletedGameModel> Handle(CompleteGame message, CancellationToken cancellationToken)
        {
            return this.games.CompleteGameAsync(message.GameId, cancellationToken);
        }
    }
}