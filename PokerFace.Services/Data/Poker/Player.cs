namespace PokerFace.Services.Data.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;

    public class Player : Entity<Guid>
    {
        public Game Game { get; set; }

        [ForeignKey(nameof(Poker.Game))]
        public Guid GameId { get; set; }

        [StringLength(CommonLengths.ShortText)]
        public string Name { get; set; }
    }
}