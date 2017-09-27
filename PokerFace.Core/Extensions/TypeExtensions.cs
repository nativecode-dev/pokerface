namespace PokerFace.Core.Extensions
{
    using System;
    using System.Linq;
    using JetBrains.Annotations;

    public static class TypeExtensions
    {
        [CanBeNull]
        public static Type GetAppDomainType([NotNull] string fullname)
        {
            var query = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                where assembly.GetName().Name.StartsWith("PokerFace")
                from type in assembly.ExportedTypes
                select type;

            return query.SingleOrDefault(type => type.FullName == fullname);
        }

        [CanBeNull]
        public static Type GetClosedInterface([NotNull] this Type source, Type type)
        {
            return source.GetInterfaces()
                .SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == type);
        }

        public static bool Implements<T>([NotNull] this Type source)
        {
            return source.Implements(typeof(T));
        }

        public static bool Implements([NotNull] this Type source, Type type)
        {
            return source.GetInterfaces().Any(i => i == type);
        }
    }
}