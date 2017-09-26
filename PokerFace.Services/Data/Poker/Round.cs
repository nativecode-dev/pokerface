namespace PokerFace.Services.Data.Poker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Round : Entity<Guid>
    {
        public Game Game { get; set; }

        [ForeignKey(nameof(Poker.Game))]
        public Guid GameId { get; set; }

        public short Number { get; set; }

        public virtual ICollection<PlayerHand> Hands { get; set; } = new List<PlayerHand>();
    }
}