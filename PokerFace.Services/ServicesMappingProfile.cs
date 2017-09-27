namespace PokerFace.Services
{
    using AutoMapper;
    using Data.Poker;
    using Models.Poker;
    using Newtonsoft.Json.Linq;

    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {
            this.CreateMap<Game, CompletedGameModel>();
            this.CreateMap<Game, GameModel>();
            this.CreateMap<Player, PlayerModel>();
            this.CreateMap<PlayerHand, PlayerHandModel>();
            this.CreateMap<Round, CompletedRoundModel>();
            this.CreateMap<Round, RoundModel>();

            this.CreateMap<JObject, GameModel>();
            this.CreateMap<JObject, PlayerModel>();
            this.CreateMap<JObject, PlayerHandModel>();
            this.CreateMap<JObject, RoundModel>();
        }
    }
}