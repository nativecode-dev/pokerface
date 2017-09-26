namespace PokerFace.Extensions
{
    using System;
    using System.IO;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class WebSocketExtensions
    {
        public static Task BroadcastTextAsync(this WebSocket socket, string data, CancellationToken cancellationToken)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<byte>(bytes);

            return socket.SendAsync(buffer, WebSocketMessageType.Text, false, cancellationToken);
        }

        public static async Task<string> GetNextTextAsync(this WebSocket socket, CancellationToken token)
        {
            var buffer = new ArraySegment<byte>(new byte[4096]);

            using (var stream = new MemoryStream())
            {
                WebSocketReceiveResult result;

                while ((result = await socket.ReceiveAsync(buffer, token).ConfigureAwait(false)).EndOfMessage == false)
                {
                    await stream.WriteAsync(buffer.Array, buffer.Offset, result.Count, token).ConfigureAwait(false);
                }

                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return default(string);
                }

                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }
    }
}