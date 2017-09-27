namespace PokerFace.Web.WebSockets.Requests
{
    using MediatR;
    using Models.Poker;

    public class NewGame : IRequest, IRequest<GameModel>
    {
        public string Name { get; set; }
    }
}