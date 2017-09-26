namespace PokerFace.Services
{
    using System;
<<<<<<< HEAD
    using System.Collections.Generic;
=======
>>>>>>> f3c3e01f465a7a0dbb03544a69e9bf85bba7524f
    using System.Threading.Tasks;
    using Models.Poker;

    public interface IGameService : IDisposable
    {
        Task CompleteGameAsync(Guid gameInstanceId);

<<<<<<< HEAD
        Task<IEnumerable<PlayerHandModel>> GetHandsAsync(Guid gameId, short round);

        Task<IEnumerable<PlayerModel>> GetPlayersAsync(Guid gameId);

        Task<PlayerModel> JoinedAsync(Guid gameInstanceId, string name);

        Task LeaveAsync(Guid gameInstanceId, Guid playerId);

        Task<GameModel> NewGameAsync();

        Task<RoundModel> NewRoundAsync(Guid gameInstanceId);

        Task PlayHandAsync(Guid gameInstanceId, Guid playerId, short value);
=======
        Task<GameModel> NewGameAsync();

        Task PlayHandAsync(Guid gameInstanceId, Guid playerId, short value);

        Task<PlayerModel> PlayerJoinedAsync(Guid gameInstanceId, string name);

        Task PlayerLeftAsync(Guid gameInstanceId, Guid playerId);

        Task<RoundModel> NewRoundAsync(Guid gameInstanceId);
>>>>>>> f3c3e01f465a7a0dbb03544a69e9bf85bba7524f
    }
}