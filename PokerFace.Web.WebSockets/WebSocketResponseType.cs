namespace PokerFace.Web.WebSockets
{
    public enum WebSocketResponseType
    {
        Default = 0,

        Broadcast = 1,

        Ignore = 2,

        Reply = WebSocketResponseType.Default
    }
}