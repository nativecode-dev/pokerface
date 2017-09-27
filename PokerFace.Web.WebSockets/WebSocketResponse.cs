namespace PokerFace.Web.WebSockets
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    [DataContract]
    public class WebSocketResponse
    {
        [DataMember]
        public object Data { get; set; }

        [DataMember]
        [DefaultValue(WebSocketResponseType.Default)]
        public WebSocketResponseType Type { get; set; }
    }

    [DataContract]
    public class WebSocketResponse<T> : WebSocketResponse
    {
        [IgnoreDataMember]
        public T Typed => (T) this.Data;
    }
}