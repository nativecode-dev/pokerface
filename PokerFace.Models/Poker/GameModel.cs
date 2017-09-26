namespace PokerFace.Models.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class GameModel : Model
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Uri Link { get; set; }
    }
}