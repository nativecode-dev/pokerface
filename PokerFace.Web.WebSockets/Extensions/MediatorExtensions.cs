namespace PokerFace.Web.WebSockets.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using JetBrains.Annotations;
    using MediatR;

    public static class MediatorExtensions
    {
        private static readonly Type GenericRequest = typeof(IRequest<>);

        private static readonly MethodInfo GenericSend =
            typeof(IMediator).GetMethods().SingleOrDefault(m => m.IsGenericMethod && m.Name == "Send");

        public static bool IsMediatorRequest([CanBeNull] this Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.Implements<IRequest>() || type.Implements(typeof(IRequest<>));
        }

        public static void Send([NotNull] this IMediator mediator, [NotNull] object request)
        {
            var type = request.GetType();

            if (type.Implements<IRequest>() == false)
            {
                throw new ArgumentException("Type does not implement IRequest.", nameof(type));
            }

            mediator.Send((IRequest) request);
        }

        public static async Task<object> Result([NotNull] this IMediator mediator, [NotNull] object request)
        {
            var type = request.GetType().GetClosedInterface(MediatorExtensions.GenericRequest);

            if (type == null)
            {
                return null;
            }

            var closed = type.GenericTypeArguments[0];
            var method = MediatorExtensions.GenericSend.MakeGenericMethod(closed);

            try
            {
                var result = (dynamic) method.Invoke(mediator, new[] { request, CancellationToken.None });
                return await result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to make send to mediator.", ex);
            }
        }
    }
}