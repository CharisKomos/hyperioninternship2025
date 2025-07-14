using System.Text.Json;

namespace MessageLib
{
    public class Message
    {
        public int InternalMessageId { get; set; }
        public JsonElement Payload { get; set; }

        public static T? ToPayload<T>(Message message)
        {
            return message.Payload.Deserialize<T>();
        }

        public static Message FromObject<T>(T obj, int id)
        {
            return new Message
            {
                InternalMessageId = id,
                Payload = JsonSerializer.SerializeToElement(obj)
            };
        }

        public static Message? FromJson(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
    }
}