namespace PokerFace.Web.WebSockets.Requests
{
    using System;
    using MediatR;
    using Models.Poker;

    public class CompleteGame : IRequest, IRequest<CompletedGameModel>
    {
        public Guid GameId { get; set; }
    }
}