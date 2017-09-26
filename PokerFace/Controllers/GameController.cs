namespace PokerFace.Controllers
{
    using System;
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