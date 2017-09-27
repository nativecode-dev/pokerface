namespace PokerFace.Models.Poker
{
    using System.Collections.Generic;

    public class CompletedGameModel : GameModel
    {
        public IEnumerable<PlayerModel> Players { get; set; } = new List<PlayerModel>();

        public IEnumerable<CompletedRoundModel> Rounds { get; set; } = new List<CompletedRoundModel>();
    }
}