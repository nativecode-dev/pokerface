namespace PokerFace.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Poker;

    public interface IGameService : IDisposable
    {
        Task CompleteGameAsync(Guid gameInstanceId);

        Task<IEnumerable<PlayerHandModel>> GetHandsAsync(Guid gameId, short round);

        Task<IEnumerable<PlayerModel>> GetPlayersAsync(Guid gameId);

        Task<PlayerModel> JoinedAsync(Guid gameInstanceId, string name);

        Task LeaveAsync(Guid gameInstanceId, Guid playerId);

        Task<GameModel> NewGameAsync();

        Task<RoundModel> NewRoundAsync(Guid gameInstanceId);

        Task PlayHandAsync(Guid gameInstanceId, Guid playerId, short value);
    }
}