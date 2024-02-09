using Microsoft.AspNetCore.SignalR;
using Serilog;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace MydemoFirst.Signal.Stream
{

    public class StreamingHub : Hub
    {


        public async IAsyncEnumerable<int> Counter(
            int count,
            int delay,
            [EnumeratorCancellation]
        CancellationToken cancellationToken)
        {
            for (var i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return i;
                await Task.Delay(delay, cancellationToken);
            }
        }

        // Method for client-to-server streaming
        public async Task UploadStream(ChannelReader<Data> dataStream)
        {


            while (await dataStream.WaitToReadAsync())
            {
                while (dataStream.TryRead(out var data))
                {
                    // Handle the dataStream item on the server
                    Console.WriteLine("--------" + data.Count);
                    Log.Information("----day al upload stream" + data.Count + data.Message);
                }
            }
        }
    }

}

public class Data
{
    public int Count { get; set; }
    public string Message { get; set; }
}
