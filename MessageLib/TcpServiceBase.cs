using MessageLib;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace MA_MessageLib
{
    public abstract class TcpServiceBase<TMessage>
    {
        private readonly TcpListener _listener;
        private readonly IEnumerable<InternalMessageId> _acceptableMessageIds;

        protected TcpServiceBase(int port, List<InternalMessageId> internalMessageIds)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
            _acceptableMessageIds = internalMessageIds;
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine($"TCP Listener started on {IPAddress.Loopback}:{((IPEndPoint)_listener.LocalEndpoint).Port}");
            new Thread(ListenLoop).Start();
        }

        private void ListenLoop()
        {
            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();
                new Thread(() => HandleClient(client)).Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string rawMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"[Raw] Received: {rawMessage}");

                try
                {
                    Message? parsedMessage = Message.FromJson(rawMessage);

                    if (parsedMessage == null || _acceptableMessageIds.Any(inMsgId => inMsgId.Id == parsedMessage.InternalMessageId))
                        throw new Exception("The parsedMessage is empty or the internalMessageId is not one of the position's acceptable ids.");

                    TMessage? payload = parsedMessage.Payload.Deserialize<TMessage>();

                    HandleMessage(payload);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Failed to parse or handle message: {ex.Message}");
                }
            }
        }

        // Derived classes must implement this
        protected abstract void HandleMessage(TMessage? message);
    }
}
