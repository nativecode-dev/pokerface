namespace PokerFace.Models.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class PlayerHandModel
    {
        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public Guid PlayerId { get; set; }

        [Required]
        [DataMember]
        public short StoryPoints { get; set; }
    }
}