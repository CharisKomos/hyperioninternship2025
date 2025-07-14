using MA_MessageLib;
using System.Text.Json;

namespace MessageLib
{
    public static class MessageSerializer
    {
        public static string Serialize(Message msg) => JsonSerializer.Serialize(msg);
        public static Message Deserialize(string json) => JsonSerializer.Deserialize<Message>(json)!;
        public static AckMessage DeserializeAck(string json) => JsonSerializer.Deserialize<AckMessage>(json)!;
    }
}