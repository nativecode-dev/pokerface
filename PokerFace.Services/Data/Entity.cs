namespace PokerFace.Services.Data
{
    using System;

    public abstract class Entity
    {
        public DateTimeOffset? DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }

    public abstract class Entity<TKey> : Entity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}