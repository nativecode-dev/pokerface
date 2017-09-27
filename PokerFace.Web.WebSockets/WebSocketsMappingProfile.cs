namespace PokerFace.Web.WebSockets
{
    using AutoMapper;
    using Newtonsoft.Json.Linq;

    public class WebSocketsMappingProfile : Profile
    {
        public WebSocketsMappingProfile()
        {
            this.CreateMap<JObject, WebSocketResponse>();
        }
    }
}