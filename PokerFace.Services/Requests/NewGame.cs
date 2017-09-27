namespace PokerFace.Services.Requests
{
    using MediatR;
    using Models.Poker;

    public class NewGame : IRequest<GameModel>
    {
        public string Name { get; set; }
    }
}