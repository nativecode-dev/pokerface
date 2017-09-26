namespace PokerFace.Controllers
{
    using System;
<<<<<<< HEAD
    using System.Collections.Generic;
=======
>>>>>>> f3c3e01f465a7a0dbb03544a69e9bf85bba7524f
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Poker;
    using Services;

    [Route("games")]
    public class GameController : Controller
    {
        private readonly IGameService game;

        public GameController(IGameService game)
        {
            this.game = game;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }

        [HttpPost]
        public Task<GameModel> NewGame()
        {
            return this.game.NewGameAsync();
        }

        [HttpPost("{gameId}/join/{name}")]
<<<<<<< HEAD
        public Task<PlayerModel> JoinGame(Guid gameId, string name)
        {
            return this.game.JoinedAsync(gameId, name);
        }

        [HttpPost("{gameId}")]
        public Task PlayHand([FromBody] PlayerHandModel model)
        {
            return this.game.PlayHandAsync(model.GameId, model.PlayerId, model.StoryPoints);
        }

        [HttpGet("{gameId}/rounds/{round}/hands")]
        public Task<IEnumerable<PlayerHandModel>> GetHands(Guid gameId, short round)
        {
            return this.game.GetHandsAsync(gameId, round);
        }

        [HttpGet("{gameId}/players")]
        public Task<IEnumerable<PlayerModel>> GetPlayers(Guid gameId)
        {
            return this.game.GetPlayersAsync(gameId);
        }

        [HttpDelete("{gameId}/players/{playerId}")]
        public Task LeaveGame(Guid gameId, Guid playerId)
        {
            return this.game.LeaveAsync(gameId, playerId);
=======
        public Task JoinGame(Guid gameId, string name)
        {
            return this.game.PlayerJoinedAsync(gameId, name);
        }

        [HttpDelete("{gameId}/players/{playerId}")]
        public Task LeaveGame(Guid gameId, Guid playerId)
        {
            return this.game.PlayerLeftAsync(gameId, playerId);
        }

        [HttpPost("{gameId}")]
        public Task PlayHand([FromBody] PlayerHandModel model)
        {
            return this.game.PlayHandAsync(model.GameId, model.PlayerId, model.StoryPoints);
>>>>>>> f3c3e01f465a7a0dbb03544a69e9bf85bba7524f
        }

        [HttpPost("{gameId}/new-round")]
        public Task NewRound(Guid gameId)
        {
            return this.game.NewRoundAsync(gameId);
        }

        [HttpDelete("{gameId}")]
        public Task CompleteGame(Guid gameId)
        {
            return this.game.CompleteGameAsync(gameId);
        }
    }
}