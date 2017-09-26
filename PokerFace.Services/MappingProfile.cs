namespace PokerFace.Services
{
    using AutoMapper;
    using Data.Poker;
    using Models.Poker;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Game, GameModel>();
            this.CreateMap<Player, PlayerModel>();
            this.CreateMap<PlayerHand, PlayerHandModel>();
            this.CreateMap<Round, RoundModel>();
        }
    }
}