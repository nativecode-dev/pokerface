namespace PokerFace.Services.Data.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlayerHand : Entity<Guid>
    {
        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(Round))]
        public Guid RoundId { get; set; }

        public short StoryPoints { get; set; }
    }
}