namespace PokerFace.Models.Poker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class PlayerModel : Model
    {
        [DataMember]
        public Guid Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}