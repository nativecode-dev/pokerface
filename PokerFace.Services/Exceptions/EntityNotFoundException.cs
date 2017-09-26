namespace PokerFace.Services.Exceptions
{
    using System;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static EntityNotFoundException Throw(string id)
        {
            return new EntityNotFoundException($"Failed to find {id}.");
        }
    }
}