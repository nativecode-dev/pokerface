namespace PokerFace.Core.Extensions
{
    using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;

    public static class LoggerExtensions
    {
        public static void LogJson<T>([NotNull] this ILogger logger, [NotNull] T instance)
        {
            logger.LogDebug(instance.ToJson());
        }
    }
}