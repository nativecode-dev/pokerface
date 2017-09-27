namespace PokerFace.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Extensions;
    using Core.Reliability;
    using Core.Services;
    using Data;
    using Data.Poker;
    using Exceptions;
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

        public async Task<CompletedGameModel> CompleteGameAsync(Guid gameId, CancellationToken token)
        {
            var game = await this.context.Games
                .Include(g => g.Players)
                .Include(g => g.Rounds)
                .Where(g => g.Id == gameId && g.State == GameState.Running)
                .OrderBy(g => g.Rounds.Select(r => r.Number))
                .SingleAsync(token)
                .Capture();

            if (game != null)
            {
                game.State = GameState.Completed;
                await this.context.SaveChangesAsync(token).Capture();

                return this.mapping.Map<CompletedGameModel>(game);
            }

            throw EntityNotFoundException.Throw(gameId.ToString());
        }

        public async Task<IEnumerable<PlayerHandModel>> GetHandsAsync(Guid gameId, short round, CancellationToken token)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from r in game.Rounds
                where r.Number == round
                from hand in r.Hands
                select this.mapping.Map<PlayerHandModel>(hand);

            return await query
                .ToListAsync(token)
                .Capture();
        }

        public async Task<IEnumerable<PlayerModel>> GetPlayersAsync(Guid gameId, CancellationToken token)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from player in game.Players
                select this.mapping.Map<PlayerModel>(player);

            return await query
                .ToListAsync(token)
                .Capture();
        }

        public async Task<IEnumerable<RoundModel>> GetRoundsAsync(Guid gameId, CancellationToken token)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from round in game.Rounds
                orderby round.Number descending
                select this.mapping.Map<RoundModel>(round);

            return await query
                .ToListAsync(token)
                .Capture();
        }

        public async Task<GameModel> NewGameAsync(string name, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = this.names.GetRandomName();
            }

            var game = new Game
            {
                Name = name,
                State = GameState.Running,
                Slug = this.names.GetRandomNameDashed(name)
            };

            this.context.Games.Add(game);

            await this.context.SaveChangesAsync(token).Capture();

            return this.mapping.Map<GameModel>(game);
        }

        public async Task PlayHandAsync(Guid gameId, Guid playerId, short value, CancellationToken token)
        {
            var query = from game in this.context.Games
                where game.Id == gameId && game.State == GameState.Running
                from round in game.Rounds
                orderby round.Number descending
                select round;

            var current = await query
                .Include(r => r.Hands)
                .SingleAsync(token)
                .Capture();

            var hand = current.Hands.SingleOrDefault(h => h.PlayerId == playerId && h.RoundId == current.Id);

            if (hand == null)
            {
                hand = new PlayerHand {PlayerId = playerId, RoundId = current.Id};
            }

            hand.StoryPoints = value;

            await this.context.SaveChangesAsync(token).Capture();
        }

        public async Task<PlayerModel> JoinedAsync(Guid gameId, string name, CancellationToken token)
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

            await this.context.SaveChangesAsync(token).Capture();

            return this.mapping.Map<PlayerModel>(player);
        }

        public async Task LeaveAsync(Guid gameId, Guid playerId, CancellationToken token)
        {
            var game = await this.context.Games
                .Include(g => g.Players)
                .SingleAsync(g => g.Id == gameId, token)
                .Capture();

            var player = game.Players.Single(p => p.Id == playerId);
            game.Players.Remove(player);

            await this.context
                .SaveChangesAsync(token)
                .Capture();
        }

        public async Task<RoundModel> NewRoundAsync(Guid gameId, CancellationToken token)
        {
            var game = await this.context.Games
                .Include(g => g.Rounds)
                .OrderBy(g => g.Rounds.Select(r => r.Number))
                .SingleAsync(g => g.Id == gameId && g.State == GameState.Running, token)
                .Capture();

            var round = new Round
            {
                GameId = gameId,
                Number = (short) (game.Rounds.Count + 1)
            };

            game.Rounds.Add(round);

            await this.context
                .SaveChangesAsync(token)
                .Capture();

            return this.mapping.Map<RoundModel>(round);
        }

        protected override void ReleaseManaged()
        {
            this.context.Dispose();
        }
    }
}