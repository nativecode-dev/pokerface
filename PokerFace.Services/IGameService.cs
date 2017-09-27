namespace PokerFace.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models.Poker;

    public interface IGameService : IDisposable
    {
        Task<CompletedGameModel> CompleteGameAsync(Guid gameInstanceId, CancellationToken token = default(CancellationToken));

        Task<IEnumerable<PlayerHandModel>> GetHandsAsync(Guid gameId, short round, CancellationToken token = default(CancellationToken));

        Task<IEnumerable<PlayerModel>> GetPlayersAsync(Guid gameId, CancellationToken token = default(CancellationToken));

        Task<IEnumerable<RoundModel>> GetRoundsAsync(Guid gameId, CancellationToken token = default(CancellationToken));

        Task<PlayerModel> JoinedAsync(Guid gameInstanceId, string name, CancellationToken token = default(CancellationToken));

        Task LeaveAsync(Guid gameInstanceId, Guid playerId, CancellationToken token = default(CancellationToken));

        Task<GameModel> NewGameAsync(string name = default(string), CancellationToken token = default(CancellationToken));

        Task<RoundModel> NewRoundAsync(Guid gameInstanceId, CancellationToken token = default(CancellationToken));

        Task PlayHandAsync(Guid gameInstanceId, Guid playerId, short value, CancellationToken token = default(CancellationToken));
    }
}