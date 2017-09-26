namespace PokerFace.Services
{
    using System;
    using System.Threading.Tasks;
    using Models.Poker;

    public interface IGameService : IDisposable
    {
        Task CompleteGameAsync(Guid gameInstanceId);

        Task<GameModel> NewGameAsync();

        Task PlayHandAsync(Guid gameInstanceId, Guid playerId, short value);

        Task<PlayerModel> PlayerJoinedAsync(Guid gameInstanceId, string name);

        Task PlayerLeftAsync(Guid gameInstanceId, Guid playerId);

        Task<RoundModel> NewRoundAsync(Guid gameInstanceId);
    }
}