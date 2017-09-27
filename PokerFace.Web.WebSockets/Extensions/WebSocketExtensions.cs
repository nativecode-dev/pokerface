namespace PokerFace.Web.WebSockets.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;

    public static class WebSocketExtensions
    {
        public static IEnumerable<Task> BroadcastText(this IEnumerable<WebSocket> sockets, string data,
            CancellationToken token)
        {
            return sockets.Select(socket => socket.SendTextAsync(data, token));
        }

        public static Task SendTextAsync(this WebSocket socket, string data, CancellationToken token)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<byte>(bytes);

            return socket.SendAsync(buffer, WebSocketMessageType.Text, false, token);
        }

        public static async Task<string> GetNextTextAsync(this WebSocket socket, CancellationToken token)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);

            using (var stream = new MemoryStream())
            {
                WebSocketReceiveResult result;

                do
                {
                    result = await socket.ReceiveAsync(buffer, token).NoCapture();
                    await stream.WriteAsync(buffer.Array, buffer.Offset, result.Count, token).NoCapture();
                }
                while (result.EndOfMessage == false);

                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return default(string);
                }

                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream, Encoding.UTF8, true, buffer.Array.Length, true))
                {
                    var message = await reader.ReadToEndAsync().NoCapture();
                    Trace.WriteLine(message);
                    return message;
                }
            }
        }
    }
}