namespace PokerFace.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Reliability;
    using Core.Services;
    using Data;
    using Data.Poker;
    using Microsoft.EntityFrameworkCore;
    using Models.Poker;

    public class GameService : Disposable, IGameService
    {
        private readonly PokerFaceDataContext context;

        private readonly IMapper mapping;

        private readonly IRandomNameService names;

        public GameService(PokerFaceDataContext context, IMapper mapping, IRandomNameService names)
        {
            this.context = context;
            this.mapping = mapping;
            this.names = names;
        }

        public Task CompleteGameAsync(Guid gameId)
        {
            var game = this.context.Games.Find(gameId);

            if (game != null)
            {
                game.State = GameState.Completed;

                return this.context.SaveChangesAsync(CancellationToken.None);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<PlayerHandModel>> GetHandsAsync(Guid gameId, short round)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from r in game.Rounds
                where r.Number == round
                from hand in r.Hands
                select this.mapping.Map<PlayerHandModel>(hand);

            return await query.ToListAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<PlayerModel>> GetPlayersAsync(Guid gameId)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from player in game.Players
                select this.mapping.Map<PlayerModel>(player);

            return await query
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<GameModel> NewGameAsync()
        {
            var name = this.names.GetRandomName();

            var game = new Game
            {
                Name = name,
                State = GameState.Running,
                ShortIdentifier = this.names.GetRandomNameDashed(name)
            };

            this.context.Games.Add(game);

            await this.context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);

            return this.mapping.Map<GameModel>(game);
        }

        public async Task PlayHandAsync(Guid gameId, Guid playerId, short value)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from round in game.Rounds
                orderby round.Number descending
                select round;

            var current = await query
                .Include(r => r.Hands)
                .SingleAsync()
                .ConfigureAwait(true);

            var hand = current.Hands.SingleOrDefault(h => h.PlayerId == playerId && h.RoundId == current.Id);

            if (hand == null)
            {
                hand = new PlayerHand {PlayerId = playerId, RoundId = current.Id};
            }

            hand.StoryPoints = value;

            await this.context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);
        }

        public async Task<PlayerModel> JoinedAsync(Guid gameId, string name)
        {
            var game = this.context.Games.Find(gameId);

            if (game == null)
            {
                throw new InvalidOperationException($"Failed to find game instance: {gameId}.");
            }

            var player = new Player
            {
                GameId = game.Id,
                Name = name
            };

            game.Players.Add(player);

            await this.context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);

            return this.mapping.Map<PlayerModel>(player);
        }

        public async Task LeaveAsync(Guid gameId, Guid playerId)
        {
            var game = await this.context.Games
                .Include(g => g.Players)
                .SingleAsync(g => g.Id == gameId)
                .ConfigureAwait(true);

            var player = game.Players.Single(p => p.Id == playerId);
            game.Players.Remove(player);

            await this.context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);
        }

        public async Task<RoundModel> NewRoundAsync(Guid gameId)
        {
            var game = await this.context.Games
                .Include(g => g.Rounds)
                .OrderBy(g => g.Rounds.Select(r => r.Number))
                .SingleAsync(g => g.Id == gameId && g.State == GameState.Running)
                .ConfigureAwait(true);

            var round = new Round
            {
                GameId = gameId,
                Number = (short) (game.Rounds.Count + 1)
            };

            game.Rounds.Add(round);

            await this.context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);

            return this.mapping.Map<RoundModel>(round);
        }

        protected override void ReleaseManaged()
        {
            this.context.Dispose();
        }
    }
}