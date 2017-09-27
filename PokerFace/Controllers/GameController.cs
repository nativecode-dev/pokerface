namespace PokerFace.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Poker;
    using Services;

    [Route("games")]
    public class GameController : ControllerBase
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

        [HttpDelete("{gameId}")]
        public Task CompleteGame(Guid gameId)
        {
            return this.game.CompleteGameAsync(gameId);
        }

        [HttpPost("{gameId}")]
        public Task Play([FromBody] PlayerHandModel model)
        {
            return this.game.PlayHandAsync(model.GameId, model.PlayerId, model.StoryPoints);
        }

        [HttpPost("{gameId}/join/{name}")]
        public Task<PlayerModel> Join(Guid gameId, string name)
        {
            return this.game.JoinedAsync(gameId, name);
        }

        [HttpPost("{gameId}/new-round")]
        public Task NewRound(Guid gameId)
        {
            return this.game.NewRoundAsync(gameId);
        }

        [HttpGet("{gameId}/rounds")]
        public Task<IEnumerable<RoundModel>> GetRounds(Guid gameId)
        {
            return this.game.GetRoundsAsync(gameId);
        }

        [HttpGet("{gameId}/rounds/{round}")]
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
        public Task Leave(Guid gameId, Guid playerId)
        {
            return this.game.LeaveAsync(gameId, playerId);
        }
    }
}