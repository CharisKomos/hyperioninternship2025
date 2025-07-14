using System.Collections.Concurrent;

namespace MessageLib
{
    public class Dispatcher
    {
        private readonly ConcurrentBag<IMessageHandler> _handlers = new();

        public void RegisterHandler(IMessageHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Dispatch(Message message)
        {
            Console.WriteLine($"[Dispatcher] Dispatching: {message.Type} - {message.Content}");
            foreach (var handler in _handlers)
            {
                handler.Handle(message);
            }
        }
    }

}
