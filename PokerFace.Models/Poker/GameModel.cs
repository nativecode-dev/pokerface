namespace PokerFace.Models.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Core;

    [DataContract]
    public class GameModel : Model
    {
        [Required]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Uri Link { get; set; }

        [DataMember]
        [StringLength(CommonLengths.ShortText)]
        public string Name { get; set; }
    }
}