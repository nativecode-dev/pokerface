namespace PokerFace.Services.Requests
{
    using System;
    using MediatR;
    using Models.Poker;

    public class NewRound : IRequest<RoundModel>
    {
        public Guid GameId { get; set; }
    }
}