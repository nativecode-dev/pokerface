namespace PokerFace.Models.Poker
{
    using System.Collections.Generic;

    public class CompletedRoundModel : RoundModel
    {
        public IEnumerable<PlayerHandModel> Hands { get; set; } = new List<PlayerHandModel>();
    }
}