using MessageLib;
using System.Net.Sockets;
using System.Text;

namespace MA_MessageLib
{
    public class MessageSender
    {
        #region Properties/Fields
        private readonly int _port;
        #endregion

        #region Constructor
        public MessageSender(int port)
        {
            _port = port;
        }
        #endregion

        #region Public methods
        public async Task SendMessageAsync(Message message, int waitForResponseInSeconds = 5)
        {
            try
            {
                using var client = new TcpClient("localhost", _port);
                using var stream = client.GetStream();
                using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                using var reader = new StreamReader(stream, Encoding.UTF8);

                // Send message
                await writer.WriteLineAsync(MessageSerializer.Serialize(message));

                // Wait for response with timeout
                var timeout = TimeSpan.FromSeconds(waitForResponseInSeconds); // change as needed
                var readTask = reader.ReadLineAsync();
                if (await Task.WhenAny(readTask, Task.Delay(timeout)) == readTask)
                {
                    // Task completed within timeout
                    string? response = await readTask;

                    // Handle the response here
                    if (!String.IsNullOrEmpty(response))
                    {
                        var ackMessageDescription = MessageSerializer.DeserializeAck(response);

                        // NOTE: Maybe do something with the acknowledgement message
                    }
                }
                else
                {
                    throw new TimeoutException("No response received within the timeout period.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending message: {ex.Message}");
            }
        }
        #endregion
    }
}