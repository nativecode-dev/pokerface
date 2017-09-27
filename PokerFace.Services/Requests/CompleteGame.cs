namespace PokerFace.Services.Requests
{
    using System;
    using MediatR;
    using Models.Poker;

    public class CompleteGame : IRequest<CompletedGameModel>
    {
        public Guid GameId { get; set; }
    }
}