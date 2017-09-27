namespace PokerFace.Services.Requests
{
    using System;
    using MediatR;

    public class LeaveGame : IRequest
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }
    }
}