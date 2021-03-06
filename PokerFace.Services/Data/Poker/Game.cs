﻿namespace PokerFace.Services.Data.Poker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Models.Poker;

    public class Game : Entity<Guid>
    {
        [StringLength(CommonLengths.ShortText)]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

        public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();

        [StringLength(CommonLengths.Identifier)]
        public string Slug { get; set; }

        public GameState State { get; set; }
    }
}