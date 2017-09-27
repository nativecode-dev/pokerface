namespace PokerFace.Services.Requests
{
    using System;
    using MediatR;
    using Models.Poker;

    public class JoinGame : IRequest<PlayerModel>
    {
        public Guid GameId { get; set; }

        public string PlayerName { get; set; }
    }
}