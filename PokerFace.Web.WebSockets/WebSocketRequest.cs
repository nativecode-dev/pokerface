namespace PokerFace.Web.WebSockets
{
    using System.Runtime.Serialization;
    using MediatR;

    [DataContract]
    public class WebSocketRequest : IRequest
    {
        [DataMember]
        public object Data { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string TypeName { get; set; }
    }

    [DataContract]
    public class WebSocketRequest<T> : WebSocketRequest, IRequest<T>
    {
        [IgnoreDataMember]
        public T Typed => (T) this.Data;
    }
}