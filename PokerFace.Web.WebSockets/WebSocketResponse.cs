namespace PokerFace.Web.WebSockets
{
    using System.Runtime.Serialization;
    using MediatR;

    [DataContract]
    public class WebSocketResponse : IRequest
    {
        [DataMember]
        public object Data { get; set; }

        [DataMember]
        public WebSocketResponseType Type { get; set; }
    }

    [DataContract]
    public class WebSocketResponse<T> : WebSocketResponse
    {
        [IgnoreDataMember]
        public T Typed => (T) this.Data;
    }
}