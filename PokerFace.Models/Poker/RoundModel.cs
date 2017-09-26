namespace PokerFace.Models.Poker
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RoundModel
    {
        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public int Number { get; set; }
    }
}