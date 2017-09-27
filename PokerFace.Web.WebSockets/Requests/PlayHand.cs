namespace PokerFace.Web.WebSockets.Requests
{
    using System;
    using MediatR;
    using Models.Poker;

    public class PlayHand : IRequest<PlayerHandModel>
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }
    }
}